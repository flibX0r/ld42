using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MVVM;

namespace Managers
{
    public class ViewModelCollection<T> where T : ModelBase
    {
        private readonly DiContainer _container;
        private readonly GameObject _parent;
        private readonly GameObject _template;
        private readonly Dictionary<T, ItemInfo> _items = new Dictionary<T, ItemInfo>();
        
        public ViewModelCollection(GameObject parent, GameObject template, DiContainer container)
        {
            _parent = parent;
            _template = template;
            _container = container;
        }

        public void AddItems(IEnumerable<T> items)
        {
            foreach (T item in items)
                AddItem(item);
        }

        public void RemoveItems(IEnumerable<T> items)
        {
            foreach (T item in items)
                RemoveItem(item);
        }

        public void AddItem(T item)
        {
            var view = Object.Instantiate(_template, _parent.transform, true);
            _container.InjectGameObject(view);
            view.name = item.InstanceName();
            _items[item] = new ItemInfo(_parent.transform, item, view);
        }

        public void RemoveItem(T item)
        {
            ItemInfo info;
            if (_items.TryGetValue(item, out info))
            {
                _items.Remove(item);
                info.Destroy();
            }
        }


        protected class ItemInfo
        {
            protected Transform _parent;
            private readonly T _model;

            public GameObject Control { get; }


            public ItemInfo(Transform parent, T model, GameObject control)
            {
                _parent = parent;
                _model = model;
                Control = control;

                Control.transform.parent = _parent;
                Control.SetDataContext(_model);
                Control.SetActive(true);
            }
            
            public void Destroy()
            {
                if (Control != null)
                    Object.Destroy(Control);
            }
        }
    }
}

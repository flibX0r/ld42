using System;
using UnityEngine;

namespace MVVM
{
    public class DataContext : MonoBehaviour
    {
        private object _value;

        public event Action<object> ValueChanged;

        public object Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;

                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }
    }

    public static class DataContextExtensions
    {
        public static DataContext GetDataContext(this GameObject gameObject)
        {
            return gameObject.GetComponentInParent<DataContext>();
        }

        public static DataContext SetDataContext(this GameObject gameObject, object value)
        {
            var dataContext = gameObject.GetComponent<DataContext>() ?? gameObject.AddComponent<DataContext>();
            dataContext.Value = value;

            return dataContext;
        }
    }
}

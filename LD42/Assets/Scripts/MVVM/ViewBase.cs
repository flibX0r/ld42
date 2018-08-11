using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using UniRx;

namespace MVVM
{
    public abstract class ViewBase<T> : MonoBehaviour where T : ModelBase
    {
        private T _viewModel;
        private bool _isQuitting;

        protected CompositeDisposable Disposer { get; } = new CompositeDisposable();

        public T ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (_viewModel == value)
                    return;

                if (_viewModel != null)
                    CleanupBindings();

                _viewModel = value;

                if (_viewModel != null)
                    InitializeBindings();
            }
        }

        protected virtual void InitializeBindings()
        {

        }

        protected virtual void CleanupBindings()
        {
            if (_isQuitting)
                return;

            Disposer.Clear();
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }
}

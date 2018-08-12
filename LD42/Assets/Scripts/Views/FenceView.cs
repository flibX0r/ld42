using System.ComponentModel;
using UnityEngine;
using Models;
using MVVM;
using Vector3 = UnityEngine.Vector3;


namespace Views
{
    public class FenceView : ViewBase<Fence>
    {
        [SerializeField] protected BoxCollider Collider;
        [SerializeField] protected MeshRenderer Mesh;

        protected override void InitializeBindings()
        {
            base.InitializeBindings();

            ViewModel.PropertyChanged += ViewModelChanged;

            transform.position = ViewModel.Position;
            if (ViewModel.IsVertical)
                transform.Rotate(Vector3.up,-90);

            IsBuildChanged();
        }

        private void ViewModelChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(Fence.IsBuilt):
                    IsBuildChanged();
                    break;
                default:
                    break;
            }
        }

        protected void IsBuildChanged()
        {
            if (ViewModel == null)
                return;

            Mesh.enabled = ViewModel.IsBuilt;
            Collider.enabled = !ViewModel.IsBuilt;
        }
    }
}

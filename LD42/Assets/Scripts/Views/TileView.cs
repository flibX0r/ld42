using System.ComponentModel;
using UnityEngine;
using Models;
using MVVM;

namespace Views
{
    public class TileView : ViewBase<Tile>
    {
        [SerializeField] protected BoxCollider Collider;
        [SerializeField] protected MeshRenderer Mesh;
        [SerializeField] protected Material[] Variant1;
        [SerializeField] protected Material[] Variant2;

        protected override void InitializeBindings()
        {
            base.InitializeBindings();

            ViewModel.PropertyChanged += ViewModelChanged;

            Mesh.materials = ViewModel.IsOdd ? Variant1 : Variant2;
            transform.position = ViewModel.Position;

            IsBuildChanged();
        }

        private void ViewModelChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(Tile.IsBuilt):
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

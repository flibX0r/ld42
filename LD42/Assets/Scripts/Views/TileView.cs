using UnityEngine;
using UniRx;
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

            ViewModel.ObserveProperty(vm => vm.IsBuilt).Subscribe(IsBuildChanged).AddTo(Disposer);

            Mesh.materials = ViewModel.IsOdd ? Variant1 : Variant2;
            transform.position = ViewModel.Position;

            IsBuildChanged(ViewModel.IsBuilt);
        }

        protected void IsBuildChanged(bool state)
        {
            if (ViewModel == null)
                return;

            Mesh.enabled = state;
            Collider.enabled = !state;
        }
    }
}

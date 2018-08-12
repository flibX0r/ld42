using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Models;
using MVVM;

namespace Managers
{
    public class FenceManager : ManagerBase
    {
        [SerializeField] private GameObject GroundTile;

        [SerializeField] private Vector2Int MaxGridSize;
        [SerializeField] private float GridScale;

        private Vector3 _gridOffset;
        private List<Fence> _fences;
        private ViewModelCollection<Fence> _fenceCollection;
        private DiContainer _container;

        public override void Install(DiContainer container, Context context)
        {
            _container = container;
            container.Bind<FenceManager>().FromInstance(this).AsSingle().NonLazy();
        }

        public override void Initialise()
        {
            // Sanity check
            if (MaxGridSize.x <= 0 || MaxGridSize.y <= 0 || GridScale <= 0)
                return;

            _fences = new List<Fence>();

            _gridOffset = new Vector3(MaxGridSize.x*GridScale, 0, MaxGridSize.y*GridScale) * -0.5f;


            for (var y = 0; y <= MaxGridSize.y; ++y)
            {
                for (var x = 0; x <= MaxGridSize.x; ++x)
                {
                    if (y < MaxGridSize.y)
                    {
                        _fences.Add(new Fence(
                            new Vector2Int(x, y),
                            new Vector3(x * GridScale, 0, y * GridScale) + _gridOffset,
                            true,
                            false)
                        );
                    }
                    if (x < MaxGridSize.x)
                    {
                        _fences.Add(new Fence(
                            new Vector2Int(x, y),
                            new Vector3(x * GridScale, 0, y * GridScale) + _gridOffset,
                            false,
                            false)
                        );
                    }
                }
            }

            _fenceCollection = new ViewModelCollection<Fence>(gameObject, GroundTile, _container);
            _fenceCollection.AddItems(_fences);
        }

    }
}


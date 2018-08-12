using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Models;
using MVVM;

namespace Managers
{
    public class TileManager : ManagerBase
    {
        [SerializeField] private GameObject GroundTile;

        [SerializeField] private Vector2Int InitialGridSize;
        [SerializeField] private Vector2Int MaxGridSize;
        [SerializeField] private float GridScale;

        private Vector3 _gridOffset;
        private List<Tile> _tiles;
        private ViewModelCollection<Tile> _tileCollection;
        private DiContainer _container;

        public override void Install(DiContainer container, Context context)
        {
            _container = container;
            container.Bind<TileManager>().FromInstance(this).AsSingle().NonLazy();
        }

        public override void Initialise()
        {
            // Sanity check
            if (MaxGridSize.x <= 0 || MaxGridSize.y <= 0 || GridScale <= 0)
                return;

            _tiles = new List<Tile>();

            _gridOffset = new Vector3(MaxGridSize.x*GridScale, 0, MaxGridSize.y*GridScale) * -0.5f;

            // Create initial grid in center
            Vector2Int initialGridOffset = (MaxGridSize - InitialGridSize);
            initialGridOffset.x /= 2;
            initialGridOffset.y /= 2;

            for (var y = 0; y < MaxGridSize.y; ++y)
            {
                for (var x = 0; x < MaxGridSize.x; ++x)
                {
                    bool isBuilt = x >= initialGridOffset.x
                                && x < initialGridOffset.x + InitialGridSize.x
                                && y >= initialGridOffset.y
                                && y < initialGridOffset.y + InitialGridSize.y;

                    _tiles.Add(new Tile(
                        new Vector2Int(x,y),
                        new Vector3(x*GridScale,0,y*GridScale) + _gridOffset, 
                        isBuilt)
                    );
                }
            }

            _tileCollection = new ViewModelCollection<Tile>(gameObject, GroundTile, _container);
            _tileCollection.AddItems(_tiles);
        }

    }
}


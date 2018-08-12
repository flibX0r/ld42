using UnityEngine;
using MVVM;

namespace Models
{
    public class Tile : ModelBase
    {
        private bool _isBuilt;

        public Vector2Int GridCoord { get; }
        public Vector3 Position { get; }

        public bool IsBuilt
        {
            get { return _isBuilt; }
            set { SetProperty(ref _isBuilt, value); }
        }

        public bool IsOdd => (GridCoord.x + GridCoord.y) % 2 > 0;

        public override string InstanceName() => $"Tile_{GridCoord.x}_{GridCoord.y}";
    
        public Tile(Vector2Int coord, Vector3 pos, bool isBuilt)
        {
            GridCoord = coord;
            Position = pos;
            IsBuilt = isBuilt;
        }
    }
}

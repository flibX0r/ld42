using UnityEngine;
using MVVM;

namespace Models
{
    public class Fence : ModelBase
    {
        private bool _isBuilt;

        public bool IsBuilt
        {
            get { return _isBuilt; }
            set { SetProperty(ref _isBuilt, value); }
        }

        public Vector2Int GridCoord { get; }
        public Vector3 Position { get; }
        public bool IsVertical { get; }

        public override string InstanceName() => $"Fence_{GridCoord.x}_{GridCoord.y}_{(IsVertical?"V":"H")}";

        public Fence(Vector2Int coord, Vector3 pos, bool isVertical, bool isBuilt)
        {
            GridCoord = coord;
            Position = pos;
            IsVertical = isVertical;
            IsBuilt = isBuilt;
        }
    }
}

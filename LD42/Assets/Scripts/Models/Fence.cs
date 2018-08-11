using UnityEngine;
using MVVM;

namespace Models
{
    public class Fence : ModelBase
    {
        public bool Built { get; set; }
        public BoxCollider Collider { get; set; }
    }
}


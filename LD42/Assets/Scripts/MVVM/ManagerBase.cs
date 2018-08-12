using UnityEngine;
using Zenject;

namespace MVVM
{
    public abstract class ManagerBase : MonoBehaviour
    {
        public virtual void Install(DiContainer container, Context context) { }

        public virtual void Initialise() { }
    }
}

using System.Collections.Generic;
using Zenject;

namespace MVVM
{
    public class ManagerInstaller : MonoInstaller<ManagerInstaller>
    {
        [Inject]
        public Context Context { get; }

        public List<ManagerBase> Managers;
        
        public override void InstallBindings()
        {
            foreach (var m in Managers)
            {
                m.Install(Container, Context);
            }

            foreach (var m in Managers)
            {
                m.Initialise();
            }
        }
    }
}

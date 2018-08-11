using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Modules;
using UnityEngine;
using Zenject;


namespace MVVM
{
    [DisallowMultipleComponent]
    public class ModuleInstaller : MonoInstaller<ModuleInstaller>
    {
        [Inject]
        public Context Context { get; }

        public override void InstallBindings()
        {
            InstallModules(Container, Context);
        }

        private static IEnumerable<IZenjectModule> GetModules(DiContainer container)
        {
            return container.ResolveAll<IZenjectModule>();
        }
        private static void InstallModules(DiContainer container, Context context)
        {
            var modules = GetModules(container);

            foreach (var module in modules)
            {
                module.Install(context);
            }
        }
    }
}

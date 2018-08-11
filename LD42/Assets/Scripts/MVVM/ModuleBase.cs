using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Modules;
using UnityEngine;
using Zenject;


namespace MVVM
{
    public abstract class ModuleBase : MonoBehaviour, IZenjectModule
    {
        protected virtual void InstallProjectBindings(ProjectContext projectContext) { }
        protected virtual void InstallSceneBindings(SceneContext sceneContext) { }
        protected virtual void InstallBindings(Context context) { }

        protected Context Context { get; private set; }
        protected DiContainer Container => Context.Container;

        private readonly HashSet<Context> _installed = new HashSet<Context>();

        public void Install(Context context)
        {
            if (!_installed.Add(context))
                return;

            Context = context;

            var projContext = context as ProjectContext;
            var sceneContext = context as SceneContext;

            if (projContext != null)
                InstallProjectBindings(projContext);

            if (sceneContext != null)
                InstallSceneBindings(sceneContext);

            InstallBindings(context);
        }
    }
}

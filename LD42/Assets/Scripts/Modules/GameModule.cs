using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVVM;
using Views;
using Zenject;

namespace Modules
{
    public class GameModule : ModuleBase
    {
        

        protected override void InstallProjectBindings(ProjectContext projectContext)
        {

        }

        protected override void InstallSceneBindings(SceneContext sceneContext)
        {
            //Container.Bind<FarmGrid>().FromNewComponentOnNewGameObject().UnderTransform(transform).AsSingle().NonLazy();

            Container.Bind<FarmGrid>().FromNewComponentOnNewGameObject().UnderTransform(transform).AsSingle().NonLazy();
        }
    }
}


using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private SerializableBoundariesSettings boundariesSettings;
        [SerializeField] private SerializableSceneSettings sceneSettings;
        [SerializeField] private SerializableAudioSettings audioSettings;
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BoundariesCrosser.BorderCrosser>()
                .AsSingle()
                .WithArguments(boundariesSettings)
                .NonLazy();
            
            Container
                .BindInterfacesTo<SerializableSceneSettings>()
                .FromInstance(sceneSettings)
                .AsSingle();

            Container
                .Bind<SceneSettings>()
                .AsSingle()
                .NonLazy();
        }
    }
}
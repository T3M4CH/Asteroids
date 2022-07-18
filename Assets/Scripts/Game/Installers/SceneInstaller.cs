using Game.BoundariesCrosser;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private int fpsRate;
        [SerializeField] private SerializableAudioSettings audioSettings;
        [SerializeField] private SerializableBoundariesSettings boundariesSettings;
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BorderCrosser>()
                .AsSingle()
                .WithArguments(boundariesSettings)
                .NonLazy();

            Container
                .Bind<SceneSettings>()
                .AsSingle()
                .WithArguments(fpsRate)
                .NonLazy();
            
            Container
                .BindInstance(audioSettings)
                .AsSingle();

            Container
                .BindMemoryPool<AudioSource, MemoryPool<AudioSource>>()
                .WithInitialSize(4)
                .FromComponentInNewPrefab(audioSettings.AudioSourcePrefab)
                .UnderTransformGroup("Audio Memory Pool");
        }
    }
}
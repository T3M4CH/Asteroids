using Game.BoundariesCrosser;
using Game.Enemies.Asteroids;
using Game.Enemies.UFO;
using Game.Player;
using Game.Settings;
using Game.Starter;
using Game.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform player;
        [SerializeField] private HealthPanel healthPanel;
        [SerializeField] private GameObject monoUfoWeapon;
        [SerializeField] private MonoPauseInput pauseInput;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private MonoAsteroid asteroidPrefab;
        [SerializeField] private MonoStarterInput starterInput;
        [SerializeField] private MonoProjectile projectilePrefab;
        [SerializeField] private SerializableAudioSettings audioSettings;
        [SerializeField] private SerializableStartSettings startSettings;
        [SerializeField] private SerializableBoundariesSettings boundariesSettings;
        
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(audioSettings)
                .AsSingle();

            Container
                .BindInstance(pauseInput)
                .AsSingle();
            
            Container
                .BindInstance(player)
                .AsSingle();
            
            Container
                .BindInterfacesTo<BorderCrosser>()
                .AsSingle()
                .WithArguments(boundariesSettings)
                .NonLazy();

            Container
                .BindInterfacesTo<ScoreSystem>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<InputSettings>()
                .AsSingle();
            
            Container
                .Bind<ScoreView>()
                .AsSingle()
                .WithArguments(scoreText)
                .NonLazy();


            Container
                .Bind<MonoUfoWeapon>()
                .FromComponentOn(monoUfoWeapon)
                .AsSingle();

            
            Container
                .BindInterfacesTo<MonoStarterInput>()
                .FromComponentInHierarchy(starterInput)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableStartSettings>()
                .FromInstance(startSettings)
                .AsSingle();

            Container
                .Bind<StartPanel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<AsteroidSpawner>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<PauseService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<HealthPanel>()
                .FromInstance(healthPanel)
                .AsSingle()
                .NonLazy();

            Container
                .BindMemoryPool<AudioSource, MemoryPool<AudioSource>>()
                .WithInitialSize(4)
                .FromComponentInNewPrefab(audioSettings.AudioSourcePrefab)
                .UnderTransformGroup("Audio Memory Pool");

            Container
                .BindMemoryPool<MonoProjectile, MemoryPool<MonoProjectile>>()
                .WithInitialSize(3)
                .FromComponentInNewPrefab(projectilePrefab)
                .UnderTransformGroup("Projectile Memory Pool");

            Container
                .BindMemoryPool<MonoAsteroid, MemoryPool<MonoAsteroid>>()
                .WithInitialSize(6)
                .FromComponentInNewPrefab(asteroidPrefab)
                .UnderTransformGroup("Asteroids Memory Pool");
        }
    }
}
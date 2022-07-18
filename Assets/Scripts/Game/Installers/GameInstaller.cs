using Game.Enemies.Asteroids;
using Game.Enemies.UFO;
using Game.Settings;
using Game.Starter;
using UnityEngine;
using Game.Player;
using Zenject;
using Game.UI;
using TMPro;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SerializableStartSettings serializableStartSettings;
        [SerializeField] private SerializablePausableMonos pausableMonos;
        [SerializeField] private HealthPanel healthPanel;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject monoUfoWeapon;
        [SerializeField] private MonoPlayerMovement playerMovement;
        [SerializeField] private MonoStarterInput starterInput;

        [Header("MemoryPoolSettings")] [SerializeField]
        private MonoProjectile projectilePrefab;

        [SerializeField] private MonoAsteroid asteroidPrefab;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MonoStarterInput>()
                .FromComponentInHierarchy(starterInput)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableStartSettings>()
                .FromInstance(serializableStartSettings)
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
                .BindInterfacesTo<SerializablePausableMonos>()
                .FromInstance(pausableMonos)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<HealthPanel>()
                .FromInstance(healthPanel)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<ScoreSystem>()
                .AsSingle();

            Container
                .Bind<ScoreView>()
                .AsSingle()
                .WithArguments(scoreText)
                .NonLazy();

            Container
                .BindInstance(playerMovement)
                .AsSingle();

            Container
                .Bind<MonoUFOWeapon>()
                .FromComponentOn(monoUfoWeapon)
                .AsSingle();

            Container
                .BindInterfacesTo<InputSettings>()
                .AsSingle();

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
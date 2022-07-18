using Game.Enemies.Asteroids.Interfaces;
using Random = UnityEngine.Random;
using Game.Starter.Interfaces;
using UnityEngine;
using Zenject;
using System;
using Game.BoundariesCrosser.Interfaces;
using Game.Constants;
using Game.Settings;
using Game.UI.Interfaces;

namespace Game.Enemies.Asteroids
{
    public class AsteroidSpawner : ISpawner, IDisposable
    {
        private IMemoryPool<MonoAsteroid> _asteroidPool;
        private IScoreSystem _scoreSystem;
        private AudioSource _audioSource;
        private IStarter _starter;
        private IBorderCrosser _borderCrosser;
        private int _count = 2;

        [Inject]
        private void Construct
        (
            MemoryPool<MonoAsteroid> asteroidPool,
            IScoreSystem scoreSystem,
            IBorderCrosser borderCrosser,
            MemoryPool<AudioSource> audioPool,
            SerializableAudioSettings audioSettings,
            IStarter starter
        )
        {
            audioSettings.Initialize();
            _asteroidPool = asteroidPool;
            _scoreSystem = scoreSystem;
            _starter = starter;
            _borderCrosser = borderCrosser;
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Explosion];
            _starter.OnGameStart += Spawn;
        }

        public void Spawn()
        {
            for (int i = 0; i < _count; i++)
            {
                var asteroid = GetAsteroid();
                asteroid.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
                asteroid.Initialize(3, _borderCrosser.Boundaries[Random.Range(0,3)], _borderCrosser, Despawn, SpawnAfterDamage, _scoreSystem);
            }
        }

        private MonoAsteroid GetAsteroid()
        {
            var asteroid = _asteroidPool.Spawn();
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }

        private void SpawnAfterDamage(MonoAsteroid prevAsteroid, int health)
        {
            _audioSource.Play();
            var asteroid = GetAsteroid();
            var transform = asteroid.transform;
            transform.eulerAngles = new Vector3(0, 0, prevAsteroid.transform.eulerAngles.z - 45);
            asteroid.Initialize(health, prevAsteroid.transform.position, _borderCrosser, Despawn, SpawnAfterDamage, _scoreSystem);
        }

        private void Despawn(MonoAsteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
            _asteroidPool.Despawn(asteroid);
            _audioSource.Play();

            if (_asteroidPool.NumActive != 0) return;
            _count += 1;
            Spawn();
        }

        public void Dispose()
        {
            _starter.OnGameStart += Spawn;
        }
    }
}
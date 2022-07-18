using Game.Enemies.Asteroids.Interfaces;
using Game.BoundariesCrosser.Interfaces;
using Random = UnityEngine.Random;
using Game.Starter.Interfaces;
using Game.Constants;
using Game.Settings;
using UnityEngine;
using Zenject;
using System;

namespace Game.Enemies.Asteroids
{
    public class AsteroidSpawner : ISpawner, IDisposable
    {
        public AsteroidSpawner
        (
            MemoryPool<MonoAsteroid> asteroidPool,
            MemoryPool<AudioSource> audioPool,
            SerializableAudioSettings audioSettings,
            SerializableGameSettings gameSettings,
            IBorderCrosser borderCrosser,
            IStarter starter
        )
        {
            audioSettings.Initialize();
            _asteroidPool = asteroidPool;
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Explosion];
            _splitAngle = gameSettings.AsteroidsSplitAngle;
            _count = gameSettings.AsteroidsStartCount;
            _borderCrosser = borderCrosser;
            _starter = starter;
            _starter.OnGameStart += Spawn;
        }
        
        private int _count;
        private readonly int _splitAngle;
        private readonly IStarter _starter;
        private readonly IMemoryPool<MonoAsteroid> _asteroidPool;
        private readonly IBorderCrosser _borderCrosser;
        private readonly AudioSource _audioSource;

        public void Spawn()
        {
            for (int i = 0; i < _count; i++)
            {
                var asteroid = GetAsteroid();
                asteroid.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
                asteroid.Initialize(3, _borderCrosser.Boundaries[Random.Range(0, 3)], Despawn, SpawnAfterDamage);
            }
        }

        private MonoAsteroid GetAsteroid()
        {
            var asteroid = _asteroidPool.Spawn();
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }

        private void SpawnAfterDamage(Transform prevAsteroid, int health)
        {
            _audioSource.Play();
            var asteroid = GetAsteroid();
            var transform = asteroid.transform;
            var asteroidEuler = prevAsteroid.eulerAngles;
            transform.eulerAngles = new Vector3(0, 0, asteroidEuler.z - _splitAngle);
            prevAsteroid.eulerAngles = new Vector3(0, 0, asteroidEuler.z + _splitAngle);
            asteroid.Initialize(health, prevAsteroid.position, Despawn, SpawnAfterDamage);
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
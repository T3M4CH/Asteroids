using Game.BoundariesCrosser.Interfaces;
using Game.Enemies.Enums;
using Game.UI.Interfaces;
using Game.Constants;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Enemies.UFO
{
    public class MonoUfoSpawner : MonoBehaviour
    {
        [SerializeField] private MonoUfo ufo;

        private bool _isSpawned;
        private float _delay;
        private float _offset;
        private float _maxY;
        private float _minY;
        private float _minTimeSpawn;
        private float _maxTimeSpawn;
        private AudioSource _audioSource;
        private DiContainer _diContainer;
        private IScoreSystem _scoreSystem;
        private IBorderCrosser _borderCrosser;
        private SerializableAudioSettings _audioSettings;

        [Inject]
        private void Construct
        (
            DiContainer diContainer,
            IBorderCrosser borderCrosser,
            IScoreSystem scoreSystem,
            MemoryPool<AudioSource> audioPool,
            SerializableGameSettings gameSettings,
            SerializableAudioSettings audioSettings
        )
        {
            _diContainer = diContainer;
            _scoreSystem = scoreSystem;
            _borderCrosser = borderCrosser;

            _audioSettings = audioSettings;
            _audioSettings.Initialize();
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Explosion];
            
            _maxY = borderCrosser.Boundaries[1].y;
            _minY = borderCrosser.Boundaries[0].y;
            _offset = (_maxY - _minY) * 0.2f;
            _maxY -= _offset;
            _minY += _offset;

            _minTimeSpawn = gameSettings.MinTimeSpawnUfo;
            _maxTimeSpawn = gameSettings.MaxTimeSpawnUfo;
        }

        private void ObjectDestroy(GameObject instance)
        {
            Destroy(instance);
            _audioSource.Play();

            _scoreSystem.AddScore(ETypeEnemy.Ufo);
            _isSpawned = false;
            _delay = Delay;
        }

        private void Spawn()
        {
            var instance = _diContainer.InstantiatePrefab(ufo).GetComponent<MonoUfo>();
            var position = new Vector2(_borderCrosser.Boundaries[Random.Range(0, 4)].x, Random.Range(_minY, _maxY));
            instance.Initialize(Random.Range(0, 2) * 2 - 1, position, ObjectDestroy);

            _isSpawned = true;
        }

        private void Update()
        {
            if (_isSpawned) return;
            _delay -= Time.deltaTime;
            if (_delay <= 0) Spawn();
        }

        private float Delay => Random.Range(_minTimeSpawn, _maxTimeSpawn);
    }
}
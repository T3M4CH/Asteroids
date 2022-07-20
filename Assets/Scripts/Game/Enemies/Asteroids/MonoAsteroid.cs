using Game.BoundariesCrosser.Interfaces;
using Random = UnityEngine.Random;
using Game.Enemies.Interfaces;
using Game.UI.Interfaces;
using Game.Enemies.Enums;
using UnityEngine;
using Zenject;
using System;

namespace Game.Enemies.Asteroids
{
    public class MonoAsteroid : MonoBehaviour, IEnemy
    {
        private float _speed = 2;
        private int _health = 3;
        private Action<MonoAsteroid> _onDestroy;
        private Action<Transform,int> _onDamage;
        private Transform _childSprite;
        private IScoreSystem _scoreSystem;
        private IBorderCrosser _borderCrosser;
        private Transform _transform;

        [Inject]
        private void Construct(IScoreSystem scoreSystem, IBorderCrosser borderCrosser)
        {
            _scoreSystem = scoreSystem;
            _borderCrosser = borderCrosser;
        }
        
        public void Initialize
        (
            int health,
            Vector3 position,
            Action<MonoAsteroid> despawnAction,
            Action<Transform,int> spawnAction
        )
        {
            _transform = transform;
            _transform.position = position;
            _onDestroy ??= despawnAction;
            _onDamage ??= spawnAction;
            _health = health;

            UpdateAsteroid(_health);
        }

        private void UpdateAsteroid(int health)
        {
            _transform.localScale = Vector2.one * (health * .5f);
            _transform.GetChild(0).eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
            _speed = (5 - health) * 0.5f;
        }

        private void AddScore(int value)
        {
            var type = value switch
            {
                1 => ETypeEnemy.SmallAsteroid,
                2 => ETypeEnemy.MediumAsteroid,
                3 => ETypeEnemy.LargeAsteroid,
                _ => throw new ArgumentOutOfRangeException()
            };
            _scoreSystem.AddScore(type);
        }

        public void GetDamage()
        {
            AddScore(_health);
            _health -= 1;
            if (_health > 0)
            {
                _onDamage.Invoke(_transform, _health);
                UpdateAsteroid(_health);
            }
            else
            {
                _onDestroy.Invoke(this);
            }
        }

        public void Despawn()
        {
            _onDestroy.Invoke(this);
        }

        private void Update()
        {
            _transform.Translate(Vector2.up * (_speed * Time.deltaTime));

            _transform.position = _borderCrosser.BoundariesCheck(_transform.position);
        }

        private void Start()
        {
            _transform = transform;
        }
    }
}
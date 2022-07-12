using System;
using Game.BorderCrosser.Interfaces;
using Game.Enemies.Interfaces;
using UnityEngine;
using Game.UI.Interfaces;
using Random = UnityEngine.Random;

namespace Game.Enemies.Asteroids
{
    public class MonoAsteroid : MonoBehaviour, IEnemy
    {
        private float _speed = 2;
        private int _health = 3;
        private Action<MonoAsteroid> _onDestroy;
        private Action<MonoAsteroid,int> _onDamage;
        private Transform _childSprite;
        private IScoreSystem _scoreSystem;
        private IBorderCrosser _borderCrosser;

        public void Initialize
        (
            int health,
            Vector3 position,
            IBorderCrosser borderCrosser,
            Action<MonoAsteroid> despawnAction,
            Action<MonoAsteroid,int> spawnAction,
            IScoreSystem scoreSystem
        )
        {
            transform.position = position;

            _scoreSystem ??= scoreSystem;
            _borderCrosser ??= borderCrosser;
            _onDestroy ??= despawnAction;
            _onDamage ??= spawnAction;
            _health = health;

            UpdateAsteroid(_health);
        }

        private void UpdateAsteroid(int health)
        {
            transform.localScale = Vector2.one * (health * .5f);
            transform.GetChild(0).eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
            _speed = (5 - health) * 0.5f;
        }

        private void AddScore(int value)
        {
            var price = value switch
            {
                1 => 100,
                2 => 50,
                3 => 20,
                _ => 0
            };
            _scoreSystem.AddScore(price);
        }

        public void GetDamage()
        {
            AddScore(_health);
            _health -= 1;
            if (_health > 0)
            {
                _onDamage.Invoke(this, _health);
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 45);
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

        public void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);

            transform.position = _borderCrosser.BoundariesCheck(transform.position);
        }
    }
}
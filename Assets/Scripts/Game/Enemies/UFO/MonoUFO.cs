using Game.BoundariesCrosser.Interfaces;
using Game.Enemies.Interfaces;
using UnityEngine;
using Zenject;
using System;

namespace Game.Enemies.UFO
{
    public class MonoUfo : MonoBehaviour, IEnemy
    {
        private event Action<GameObject> OnDamage = _ => { };
        private IBorderCrosser _boundaries;
        private Transform _transform;
        private int _direction;

        [Inject]
        private void Construct
        (
            IBorderCrosser borderCrosser
        )
        {
            _boundaries = borderCrosser;
        }

        public void Initialize
        (
            int direction,
            Vector2 spawnPosition,
            Action<GameObject> callback
        )
        {
            _transform = transform;
            _transform.position = spawnPosition;
            _direction = direction;
            OnDamage += callback;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * _direction * 2 * Time.deltaTime);

            _transform.position = _boundaries.BoundariesCheck(_transform.position);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IEnemy enemy)) return;
            enemy.GetDamage();
            OnDamage.Invoke(gameObject);
        }

        public void GetDamage()
        {
            OnDamage.Invoke(gameObject);
        }

        public void Despawn()
        {
            OnDamage.Invoke(gameObject);
        }
    }
}
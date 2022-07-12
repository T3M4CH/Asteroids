using Game.BorderCrosser.Interfaces;
using Game.Enemies.Interfaces;
using Game.Player.Interfaces;
using UnityEngine;
using Zenject;
using System;
using Game.Settings;
using Random = UnityEngine.Random;

namespace Game.Enemies.UFO
{
    public class MonoUFO : MonoBehaviour, IEnemy
    {
        private event Action<GameObject> OnDamage = _ => { };
        private IBorderCrosser _boundaries;
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
            transform.position = spawnPosition;
            _direction = direction;
            OnDamage += callback;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * _direction * 2 * Time.deltaTime);

            transform.position = _boundaries.BoundariesCheck(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IDamagable damagable)) return;
            damagable.GetDamage();
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
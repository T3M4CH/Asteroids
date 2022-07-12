using System;
using Game.Player.Interfaces;
using UnityEngine;

namespace Game.Player
{
    public class MonoProjectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        private Transform _owner;
        private bool isHit;
        private float _speed;
        private Vector3 _position;
        private Vector3[] _boundaries;
        private Quaternion _rotation;
        private Action<MonoProjectile> _onCollision;

        public void Initialize
        (
            float speed,
            Color color,
            Transform owner,
            Vector3 position,
            Quaternion rotation,
            Action<MonoProjectile> action,
            Vector3[] corners
        )
        {
            sprite.color = color;
            isHit = false;
            _speed = speed;
            _owner = owner;
            transform.position = position;
            transform.rotation = rotation;
            _onCollision ??= action;
            _boundaries = corners;
        }

        private void Update()
        {
            transform.Translate(Vector2.up * (_speed * Time.deltaTime));

            CheckBoundries();
        }

        private void CheckBoundries()
        {
            var position = transform.position;

            if (position.x < _boundaries[0].x || position.x > _boundaries[2].x ||
                position.y < _boundaries[0].y || position.y > _boundaries[1].y)
            {
                _onCollision.Invoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform == _owner) return;
            if (!col.TryGetComponent(out IDamagable damagable) || isHit) return;
            isHit = true;
            damagable.GetDamage();
            _onCollision.Invoke(this);
        }
    }
    
}
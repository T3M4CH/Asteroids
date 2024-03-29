﻿using Game.BoundariesCrosser.Interfaces;
using Game.Player.Interfaces;
using UnityEngine;
using Zenject;
using System;

namespace Game.Player
{
    public class MonoProjectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        
        private bool _isHit;
        private float _speed;
        private float _distance;
        private float _passedWay;
        private Vector3 _position;
        private Quaternion _rotation;
        private Transform _owner;
        private IBorderCrosser _borderCrosser;
        private Action<MonoProjectile> _onCollision;

        [Inject]
        private void Construct(IBorderCrosser borderCrosser)
        {
            _borderCrosser = borderCrosser;
            _distance = Vector2.Distance(borderCrosser.Boundaries[0] ,borderCrosser.Boundaries[2]);
        }

        public void Initialize
        (
            float speed,
            Color color,
            Transform owner,
            Vector3 position,
            Quaternion rotation,
            Action<MonoProjectile> action
        )
        {
            _passedWay = 0;
            sprite.color = color;
            _isHit = false;
            _speed = speed;
            _owner = owner;
            transform.position = position;
            transform.rotation = rotation;
            _onCollision ??= action;
        }

        private void Update()
        {
            transform.Translate(Vector2.up * (_speed * Time.deltaTime));

            _passedWay += _speed * Time.deltaTime;

            if (_passedWay > _distance)
            {
                _onCollision.Invoke(this);
                return;
            }

            CheckBoundries();
        }

        private void CheckBoundries()
        {
            transform.position = _borderCrosser.BoundariesCheck(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform == _owner) return;
            if (!col.TryGetComponent(out IDamagable damagable) || _isHit) return;
            _isHit = true;
            damagable.GetDamage();
            _onCollision.Invoke(this);
        }
    }
    
}
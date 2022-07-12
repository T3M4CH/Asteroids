using UnityEngine;
using System;
using Game.Enemies.Interfaces;
using Game.Player.Interfaces;

namespace Game.Player
{
    public class MonoPhysicsCollision : MonoBehaviour, IDamagable
    {
        public Action OnCollision = () => {};

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IEnemy enemy)) return;
            enemy.Despawn();
            OnCollision.Invoke();
        }

        public void GetDamage()
        {
            OnCollision.Invoke();
        }
    }
}
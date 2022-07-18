using Game.Settings;
using Game.General;
using UnityEngine;
using Zenject;

namespace Game.Enemies.UFO
{
    public class MonoUfoWeapon : MonoWeapon
    {
        private Transform _player;
        private float _delay = 0;
        private float _minDelay;
        private float _maxDelay;
        

        [Inject]
        private void Construct(Transform player, SerializableGameSettings gameSettings)
        {
            _player = player;
            _minDelay = gameSettings.MinShootDelayUfo;
            _maxDelay = gameSettings.MaxShootDelayUfo;
        }

        public override void Fire()
        {
            var direction = (_player.position - transform.position).normalized;
            var rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg));
            var position = transform.position;
            var projectile = ProjectilePool.Spawn();

            projectile.gameObject.SetActive(true);
            projectile.Initialize(4, Color.red, transform, position, rotation, Despawn);
        }

        public void Update()
        {
            _delay -= Time.deltaTime;

            if (!(_delay <= 0)) return;
            Fire();
            _delay = Delay;
        }

        private float Delay => Random.Range(_minDelay, _maxDelay);
    }
}
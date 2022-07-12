using Game.General;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Enemies.UFO
{
    public class MonoUFOWeapon : MonoWeapon
    {
        private MonoPlayerMovement _playerMovement;
        private float _delay;

        [Inject]
        private void Construct(MonoPlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        public override void Fire()
        {
            var direction = (_playerMovement.transform.position - transform.position).normalized;
            var rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg));
            var position = transform.position;
            var projectile = ProjectilePool.Spawn();

            projectile.gameObject.SetActive(true);
            projectile.Initialize(4, Color.red, transform, position, rotation, Despawn, Corners);
        }

        public void Update()
        {
            _delay -= Time.deltaTime;

            if (_delay <= 0)
            {
                Fire();
                _delay = Delay;
            }
        }

        private float Delay => Random.Range(2, 6);
    }
}
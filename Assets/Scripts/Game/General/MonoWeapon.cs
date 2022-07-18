using Game.BoundariesCrosser.Interfaces;
using Game.Settings;
using UnityEngine;
using Game.Player;
using Zenject;

namespace Game.General
{
    public abstract class MonoWeapon : MonoBehaviour
    {
        protected MemoryPool<MonoProjectile> ProjectilePool;
        protected IMemoryPool<AudioSource> AudioPool;
        protected SerializableAudioSettings AudioSettings;
        protected IBorderCrosser BorderCrosser;

        [Inject]
        private void Construct
        (
            MemoryPool<MonoProjectile> projectilePool,
            MemoryPool<AudioSource> audioPool,
            IBorderCrosser borderCrosser,
            SerializableAudioSettings audioSettings
        )
        {
            ProjectilePool = projectilePool;
            AudioPool = audioPool;
            BorderCrosser = borderCrosser;
            AudioSettings = audioSettings;
        }

        public abstract void Fire();

        protected void Despawn(MonoProjectile sender)
        {
            ProjectilePool.Despawn(sender);
            sender.gameObject.SetActive(false);
        }
    }
}
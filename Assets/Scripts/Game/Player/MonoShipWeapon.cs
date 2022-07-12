using System.Linq;
using Game.Constants;
using Game.General;
using UnityEngine;

namespace Game.Player
{
    public class MonoShipWeapon : MonoWeapon
    {
        private readonly float[] _cooldown = new float[3];
        
        public override void Fire()
        {
            ResetTimer();

            var rot = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z));
            var position = transform.position + transform.up / 2;
            var projectile = ProjectilePool.Spawn();
            projectile.gameObject.SetActive(true);
            projectile.Initialize(4, Color.green, transform, position, rot, Despawn, Corners);

            var audio = AudioPool.Spawn();
            audio.PlayOneShot(AudioSettings.AudioStorage[AudioConstants.Fire]);
            AudioPool.Despawn(audio);
        }


        private void ResetTimer()
        {
            for (var i = 0; i < _cooldown.Length; i++)
            {
                if (!(_cooldown[i] <= 0)) continue;
                _cooldown[i] = 1;
                break;
            }
        }

        private void Update()
        {
            for (int i = 0; i < _cooldown.Length; i++)
            {
                if (_cooldown[i] > 0)
                {
                    _cooldown[i] -= Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _cooldown.Any(x => x <= 0))
            {
                Fire();
            }
        }
    }
}
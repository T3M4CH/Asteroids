using Game.Settings.Interfaces;
using Game.Settings.Enums;
using Game.Constants;
using Game.Settings;
using Game.General;
using System.Linq;
using UnityEngine;
using Zenject;
using System;

namespace Game.Player
{
    public class MonoShipWeapon : MonoWeapon
    {
        private bool _fire;
        private float[] _bulletCount;
        private IInputSettings _inputSettings;

        [Inject]
        private void Construct(IInputSettings inputSettings, SerializableGameSettings gameSettings)
        {
            _inputSettings = inputSettings;
            _bulletCount = new float[gameSettings.BulletsPerSecond];
        }

        public override void Fire()
        {
            ResetTimer();

            var rot = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z));
            var position = transform.position + transform.up / 2;
            var projectile = ProjectilePool.Spawn();
            projectile.gameObject.SetActive(true);
            projectile.Initialize(4, Color.green, transform, position, rot, Despawn);

            var audio = AudioPool.Spawn();
            audio.PlayOneShot(AudioSettings.AudioStorage[AudioConstants.Fire]);
            AudioPool.Despawn(audio);
        }


        private void ResetTimer()
        {
            for (var i = 0; i < _bulletCount.Length; i++)
            {
                if (!(_bulletCount[i] <= 0)) continue;
                _bulletCount[i] = 1;
                break;
            }
        }

        private void Update()
        {
            for (int i = 0; i < _bulletCount.Length; i++)
            {
                if (_bulletCount[i] > 0)
                {
                    _bulletCount[i] -= Time.deltaTime;
                }
            }

            if (!_bulletCount.Any(x => x <= 0)) return;
            switch (_inputSettings.EInputScheme)
            {
                case EInputScheme.Keyboard:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Fire();
                    }

                    break;
                case EInputScheme.KeyboardMouse:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Fire();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
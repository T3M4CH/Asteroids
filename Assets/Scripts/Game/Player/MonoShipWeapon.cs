using System;
using System.Linq;
using Game.Constants;
using Game.General;
using Game.Settings.Enums;
using Game.Settings.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class MonoShipWeapon : MonoWeapon
    {
        private readonly float[] _cooldown = new float[3];

        private IInputSettings _inputSettings;
        private bool _fire;

        [Inject]
        private void Construct(IInputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }

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

            if (!_cooldown.Any(x => x <= 0)) return;
            switch (_inputSettings.InputScheme)
            {
                case InputScheme.Keyboard:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Fire();
                    }

                    break;
                case InputScheme.KeyboardMouse:
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
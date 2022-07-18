using Game.Settings.Interfaces;
using Game.Settings.Enums;
using UnityEngine;
using Game.Utils;
using Zenject;
using System;
using Game.Settings;

namespace Game.Player
{
    public class MonoPlayerMovement : MonoBehaviour
    {
        private ShipAccelerator _shipAccelerator;
        private ShipRotator _shipRotator;
        private EngineSound _engineSound;

        private float _rotation;
        private float _acceleration;
        private Camera _camera;
        private IInputSettings _inputSettings;
        private IMemoryPool<AudioSource> _audioPool;
        private SerializableAudioSettings _audioSettings;

        [Inject]
        private void Construct
        (
            EngineSound engineSound,
            ShipAccelerator shipAccelerator,
            ShipRotator shipRotator,
            IInputSettings inputSettings
        )
        {
            _engineSound = engineSound;
            _shipRotator = shipRotator;
            _shipAccelerator = shipAccelerator;
            _inputSettings = inputSettings;
        }

        private void Update()
        {
            switch (_inputSettings.EInputScheme)
            {
                case EInputScheme.Keyboard:
                    _rotation = Input.GetAxisRaw("Horizontal") * _shipRotator.AngleVelocity;
                    _acceleration = Input.GetAxisRaw("Vertical");
                    break;
                case EInputScheme.KeyboardMouse:
                    var direction = (_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                    _rotation = (Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg - transform.eulerAngles.z)
                        .FixAngle();
                    _acceleration = Input.GetAxisRaw("Vertical");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _shipRotator.Rotate(_rotation);
            _shipAccelerator.Accelerate(_acceleration);
            _engineSound.PlaySound(_acceleration);
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }
    }
}
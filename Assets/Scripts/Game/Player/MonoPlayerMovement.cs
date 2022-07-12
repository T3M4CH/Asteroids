using Game.Settings.Interfaces;
using Game.Settings.Enums;
using UnityEngine;
using Game.Utils;
using Zenject;
using System;
using Game.BorderCrosser.Interfaces;
using Game.Constants;
using Game.Settings;

namespace Game.Player
{
    public class MonoPlayerMovement : MonoBehaviour
    {
        [SerializeField] private ShipAccelerator shipAccelerator;
        [SerializeField] private ShipRotator shipRotator;

        private float _rotation;
        private float _acceleration;
        private Camera _camera;
        private EngineSound _engineSound;
        private IBorderCrosser _borderCrosser;
        private IInputSettings _inputSettings;
        private IMemoryPool<AudioSource> _audioPool;
        private SerializableAudioSettings _audioSettings;

        [Inject]
        private void Construct
        (
            IInputSettings inputSettings,
            IBorderCrosser borderCrosser,
            MemoryPool<AudioSource> audioPool,
            SerializableAudioSettings audioSettings
        )
        {
            _inputSettings = inputSettings;
            _borderCrosser = borderCrosser;
            _audioSettings = audioSettings;
            _audioPool = audioPool;
        }

        private void Update()
        {
            switch (_inputSettings.InputScheme)
            {
                case InputScheme.Keyboard:
                    _rotation = Input.GetAxisRaw("Horizontal") * shipRotator.AngleVelocity;
                    _acceleration = Input.GetAxisRaw("Vertical");
                    break;
                case InputScheme.KeyboardMouse:
                    var direction = (_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                    _rotation = (Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg - transform.eulerAngles.z)
                        .FixAngle();
                    _acceleration = Input.GetAxisRaw("Vertical");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            shipRotator.Rotate(_rotation);
            shipAccelerator.Accelerate(_acceleration);
            _engineSound.Accelerate(_acceleration);
            transform.position = _borderCrosser.BoundariesCheck(transform.position);
        }

        private void Start()
        {
            var source = _audioPool.Spawn();
            _engineSound = new EngineSound(source, _audioSettings.AudioStorage[AudioConstants.Thrust]);
            _camera = Camera.main;
        }
    }
}
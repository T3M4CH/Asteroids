using Game.BoundariesCrosser.Interfaces;
using Game.Settings;
using UnityEngine;

namespace Game.Player
{
    public class ShipAccelerator
    {
        public ShipAccelerator(SerializableGameSettings gameSettings, IBorderCrosser borderCrosser, Transform player)
        {
            _ship = player;
            _maxSpeed = gameSettings.ShipMaxSpeed;
            _deceleration = gameSettings.ShipDeceleration;
            _acceleration = gameSettings.ShipAcceleration;
            _borderCrosser = borderCrosser;
        }

        private readonly float _deceleration;
        private readonly float _acceleration;
        private readonly float _maxSpeed ;
        private readonly Transform _ship;
        private readonly IBorderCrosser _borderCrosser;

        private Vector2 _force;

        public void Accelerate(float vertical)
        {
            if (vertical > 0)
            {
                _force = Vector2.ClampMagnitude(_ship.up * (_acceleration * Time.deltaTime) + (Vector3)_force, _maxSpeed);
            }
            else
            {
                _force = Vector2.MoveTowards(_force, Vector2.zero, Time.deltaTime * _deceleration);
            }

            _ship.transform.position += (Vector3)_force * Time.deltaTime;
            _ship.position = _borderCrosser.BoundariesCheck(_ship.position);
        }
    }
}
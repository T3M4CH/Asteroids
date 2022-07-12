using UnityEngine;
using System;

namespace Game.Player
{
    [Serializable]
    public class ShipAccelerator
    {
        [SerializeField] [Range(0, 1)] private float frictionValue;
        [SerializeField] private float thrustSpeed;
        [SerializeField] private Transform ship;
        
        private float _speed;
        private Vector2 _direction;

        public void Accelerate(float vertical)
        {
            if (vertical > 0)
            {
                _speed = thrustSpeed * Time.deltaTime;
                _direction = ship.up;
            }
            else
            {
                _speed *= frictionValue;
            }

            ship.transform.Translate(_direction * _speed, Space.World);
        }
    }
}
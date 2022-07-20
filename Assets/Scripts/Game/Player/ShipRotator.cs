using Game.Settings;
using UnityEngine;

namespace Game.Player
{
    public class ShipRotator
    {
        public ShipRotator(SerializableGameSettings gameSettings, Transform player)
        {
            AngleVelocity = gameSettings.ShipAngleVelocity;
            _ship = player;
        }
        
        public readonly float AngleVelocity;
        private readonly Transform _ship;

        public void Rotate(float direction)
        {
            direction = Mathf.Clamp(direction, -AngleVelocity, AngleVelocity);
            _ship.Rotate(Vector3.forward * (direction * Mathf.Rad2Deg * Time.deltaTime));
        }

    }
}
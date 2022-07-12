using System;
using UnityEngine;

namespace Game.Player
{
    [Serializable]
    public class ShipRotator
    {
        [SerializeField] private Transform ship;
        public void Rotate(float direction)
        {
            direction = Mathf.Clamp(direction, -AngleVelocity, AngleVelocity);
            ship.Rotate(Vector3.forward * direction);
        }

        [field: SerializeField]
        public float AngleVelocity
        {
            get;
            private set;
        }
    }
}

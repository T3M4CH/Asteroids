using Game.BoundariesCrosser.Interfaces;
using Game.Settings;
using UnityEngine;

namespace Game.BoundariesCrosser
{
    public class BorderCrosser : IBorderCrosser

    {
        public BorderCrosser(SerializableBoundariesSettings boundariesSettings)
        {
            boundariesSettings.Initialize();
            _boundaries = boundariesSettings.CornerPosition;
        }
        
        private readonly Vector3[] _boundaries;

        public Vector2 BoundariesCheck(Vector2 position)
        {
            if (position.x < _boundaries[0].x || position.x > _boundaries[2].x)
            {
                position.x = position.x < _boundaries[0].x ? _boundaries[2].x : _boundaries[0].x;
            }

            if (position.y < _boundaries[0].y || position.y > _boundaries[1].y)
            {
                position.y = position.y < _boundaries[0].y ? _boundaries[1].y : _boundaries[0].y;
            }

            return position;
        }

        public Vector3[] Boundaries => _boundaries;
    }
}
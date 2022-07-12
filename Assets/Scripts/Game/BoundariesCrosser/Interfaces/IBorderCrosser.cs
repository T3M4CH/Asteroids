using UnityEngine;

namespace Game.BorderCrosser.Interfaces
{
    public interface IBorderCrosser
    {
        Vector2 BoundariesCheck(Vector2 position);

        Vector3[] Boundaries
        {
            get;
        }
    }
}

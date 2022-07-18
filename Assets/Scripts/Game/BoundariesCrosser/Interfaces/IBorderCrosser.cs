using UnityEngine;

namespace Game.BoundariesCrosser.Interfaces
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

using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface IBoundariesSettings
    {
        RectTransform Playground
        {
            get;
        }

        Vector3[] CornerPosition
        {
            get;
        }
    }
}

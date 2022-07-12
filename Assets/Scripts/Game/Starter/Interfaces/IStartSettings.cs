using System.Collections.Generic;
using UnityEngine;

namespace Game.Starter.Interfaces
{
    public interface IStartSettings
    {
        RectTransform StartText
        {
            get;
        }

        List<GameObject> GameObjects
        {
            get;
        }
    }
}

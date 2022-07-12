using Game.Starter.Interfaces;
using Game.Enemies.Asteroids;
using UnityEngine;
using System;
using System.Collections.Generic;
using Game.Player;

namespace Game.Starter
{
    [Serializable]
    public class SerializableStartSettings : IStartSettings
    {
        [field:SerializeField]
        public RectTransform StartText
        {
            get;
            private set;
        }

        [field: SerializeField]
        public List<GameObject> GameObjects
        {
            get;
            private set;
        }
    }
}

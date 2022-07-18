using System.Collections.Generic;
using Game.Starter.Interfaces;
using UnityEngine;
using System;

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

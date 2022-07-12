using Game.Options.Interfaces;
using UnityEngine;
using System;

namespace Game.Settings
{
    [Serializable]
    public class SerializableSceneSettings : ISceneSettings
    {
        [field: SerializeField]
        public int FPSRate
        {
            get;
            private set;
        }
    }
}

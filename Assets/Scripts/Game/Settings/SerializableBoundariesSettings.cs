using Game.Settings.Interfaces;
using UnityEngine;
using Zenject;
using System;

namespace Game.Settings
{
    [Serializable]
    public class SerializableBoundariesSettings : IBoundariesSettings, IInitializable
    {
        public void Initialize()
        {
            Playground.GetWorldCorners(CornerPosition);
        }

        [field: SerializeField]
        public RectTransform Playground
        {
            get;
            private set;
        }

        public Vector3[] CornerPosition { get; } = new Vector3[4];
    }
}
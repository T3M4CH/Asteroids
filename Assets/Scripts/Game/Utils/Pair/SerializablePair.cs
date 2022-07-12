using Game.Utils.Pair.Interfaces;
using UnityEngine;
using System;

namespace Game.Utils.Pair
{
    [Serializable]
    public struct SerializablePair<TKey, TValue> : IPair<TKey, TValue>
    {
        [SerializeField] private TKey key;
        [SerializeField] private TValue value;

        public TKey Key => key;
        public TValue Value => value;
    }
}
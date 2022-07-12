using System.Collections.Generic;
using Game.Utils.Pair;
using UnityEngine;
using System;
using System.Linq;
using Zenject;

namespace Game.Settings
{
    [Serializable]
    public class SerializableAudioSettings : IInitializable
    {
        [SerializeField] private List<SerializablePair<string, AudioClip>> audioStorage;

        [field: SerializeField] public AudioSource AudioSourcePrefab { get; private set; }

        public Dictionary<string, AudioClip> AudioStorage = new Dictionary<string, AudioClip>();

        public void Initialize()
        {
            if (AudioStorage.Any()) return;
            foreach (var audio in audioStorage)
            {
                AudioStorage.Add(audio.Key, audio.Value);
            }
        }
    }
}
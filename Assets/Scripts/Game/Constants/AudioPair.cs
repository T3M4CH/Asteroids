using System;
using UnityEngine;

namespace Game.Constants
{
    [Serializable]
    public class AudioPair
    {
        [SerializeField] private string clipName;
        [SerializeField] private AudioClip clip;
    }
}

using Game.Constants;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class EngineSound
    {
        public EngineSound(MemoryPool<AudioSource> audioPool, SerializableAudioSettings audioSettings)
        {
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Thrust];
        }

        private readonly AudioSource _audioSource;

        public void PlaySound(float vertical)
        {
            if (vertical > 0) _audioSource.Play();
            else _audioSource.Stop();
        }
    }
}

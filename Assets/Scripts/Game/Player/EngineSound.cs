using UnityEngine;

namespace Game.Player
{
    public class EngineSound
    {
        public EngineSound(AudioSource audioSource, AudioClip clip)
        {
            _audioSource = audioSource;
            _audioSource.clip = clip;
        }

        private readonly AudioSource _audioSource;

        public void Accelerate(float vertical)
        {
            if (vertical > 0) _audioSource.Play();
            else _audioSource.Stop();
        }
    }
}

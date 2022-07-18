using Game.Player;
using UnityEngine;
using System;

namespace Game.UI
{
    public class PauseService : IDisposable
    {
        public PauseService(MonoPauseInput pauseInput, Transform player)
        {
            _pauseInput = pauseInput;
            _playerMovement = player.GetComponent<MonoPlayerMovement>();
            _playerWeapon = player.GetComponent<MonoShipWeapon>();
            
            _pauseInput.OnPause += ChangeState;
        }
        
        private readonly MonoPlayerMovement _playerMovement;
        private readonly MonoShipWeapon _playerWeapon;
        private readonly MonoPauseInput _pauseInput;

        private void ChangeState(bool value)
        {
            _playerMovement.enabled = value;
            _playerWeapon.enabled = value;
        }
        
        public void Dispose()
        {
            _pauseInput.OnPause -= ChangeState;
        }
    }
}
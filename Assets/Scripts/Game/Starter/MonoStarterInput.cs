using Game.Starter.Interfaces;
using UnityEngine;
using Zenject;
using System;

namespace Game.Starter
{
    public class MonoStarterInput : MonoBehaviour, IStarter
    {
        public event Action OnGameStart = () => { };

        private IStartSettings _startSettings;

        [Inject]
        private void Construct(IStartSettings startSettings)
        {
            _startSettings = startSettings;
        }
        
        private void Update()
        {
            if (!Input.anyKey) return;
            _startSettings.GameObjects.ForEach(x => x.SetActive(true));
            OnGameStart.Invoke();
            enabled = false;
        }

        private void Start()
        {
            _startSettings.StartText.gameObject.SetActive(true);
        }
    }
}
using System;
using Game.Starter.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Starter
{
    public class MonoStarterInput : MonoBehaviour
    {
        [Inject]
        private void Construct(IStarter starter, IStartSettings startSettings)
        {
            _starter = starter;
            _startSettings = startSettings;
        }

        private IStartSettings _startSettings;
        private IStarter _starter;

        private void Update()
        {
            if (!Input.anyKey) return;
            _startSettings.GameObjects.ForEach(x => x.SetActive(true));
            _starter.OnGameStart.Invoke();
            enabled = false;
        }

        private void Start()
        {
            _startSettings.StartText.gameObject.SetActive(true);
        }
    }
}
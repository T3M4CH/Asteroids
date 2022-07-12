using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using Game.General;

namespace Game.UI
{
    [Serializable]
    public class SerializablePausableMonos : IInitializable, IDisposable, IRepository<MonoBehaviour>
    {
        [SerializeField] private MonoPauseInput pauseInput;

        private void ChangeState(bool value)
        {
            List.ForEach(x => { x.enabled = value; });
        }

        public void Initialize()
        {
            pauseInput.OnPause += ChangeState;
        }

        public void Dispose()
        {
            pauseInput.OnPause -= ChangeState;
        }

        [field: SerializeField]
        public List<MonoBehaviour> List
        {
            get;
            private set;
        }
    }
}
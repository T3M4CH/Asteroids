using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Game.Player;
using Zenject;
using System;

namespace Game.UI
{
    [Serializable]
    public class HealthPanel : IInitializable, IDisposable
    {
        [SerializeField] private MonoPhysicsCollision physicsCollision;
        [SerializeField] private MonoMenu monoMenu;
        
        private int _health = 4;
        
        public void RemoveHeart()
        {
            _health -= 1;
            if (_health < 0) return;
            List[_health].gameObject.SetActive(false);
            if (_health != 0) return;
            monoMenu.gameObject.SetActive(true);
            monoMenu.SetupRestart();
        }

        public void Initialize()
        {
            physicsCollision.OnCollision += RemoveHeart;
        }
        
        public void Dispose()
        {
            physicsCollision.OnCollision -= RemoveHeart;
        }
        
        [field: SerializeField]
        public List<Image> List
        { 
            get;
            private set;
        }
    }
}
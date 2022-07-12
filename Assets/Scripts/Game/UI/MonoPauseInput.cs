using System;
using UnityEngine;

namespace Game.UI
{
    public class MonoPauseInput : MonoBehaviour
    {
        public Action<bool> OnPause = _ => { };

        [SerializeField] private GameObject menu;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            var scale = Time.timeScale > 0 ? 0 : 1;
            var isZero = scale == 0;
            menu.SetActive(isZero);
            OnPause.Invoke(!isZero);
            Time.timeScale = scale;
        }
    }
}

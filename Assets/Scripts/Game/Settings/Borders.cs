using System;
using UnityEngine;

namespace Game.Settings
{
    public class Borders : MonoBehaviour
    {
        [SerializeField] private Vector2 boundaries;
        [SerializeField] private RectTransform screen;
        [SerializeField] private RectTransform image;
        
        private void Start()
        {
            var v = new Vector3[4];
            screen.GetWorldCorners(v);

            boundaries = screen.sizeDelta / 2;
        }
    }
}
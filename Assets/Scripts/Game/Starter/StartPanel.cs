using Game.Starter.Interfaces;
using DG.Tweening;
using UnityEngine;
using System;

namespace Game.Starter
{
    public class StartPanel : IDisposable
    {
        public StartPanel(IStartSettings startSettings, IStarter starter)
        {
            _text = startSettings.StartText;
            _starter = starter;
            
            _starter.OnGameStart += DeleteText;
            _text.DOAnchorPosY(180, 1).SetLoops(-1, LoopType.Yoyo);
        }
        
        private readonly IStarter _starter;
        private readonly RectTransform _text;
        
        private void DeleteText()
        {
            _text.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _starter.OnGameStart -= DeleteText;
        }
    }
}

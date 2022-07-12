using System;
using Game.UI.Interfaces;

namespace Game.UI
{
    public class ScoreSystem : IScoreSystem
    {
        public event Action OnChanged = () => {};
        
        public void AddScore(int value)
        {
            Score += value;
            OnChanged.Invoke();
        }

        public int Score { get; private set; }
    }
}

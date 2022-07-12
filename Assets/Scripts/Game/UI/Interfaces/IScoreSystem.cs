using System;

namespace Game.UI.Interfaces
{
    public interface IScoreSystem
    {
        event Action OnChanged;
        void AddScore(int value);
        int Score { get; }
    }
}
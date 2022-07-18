using System;
using Game.Enemies.Enums;

namespace Game.UI.Interfaces
{
    public interface IScoreSystem
    {
        event Action OnChanged;
        void AddScore(ETypeEnemy enemyType);
        int Score { get; }
    }
}
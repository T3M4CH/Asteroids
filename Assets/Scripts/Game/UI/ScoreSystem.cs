using System.Collections.Generic;
using Game.Enemies.Enums;
using Game.UI.Interfaces;
using Game.Settings;
using Game.Utils;
using System;

namespace Game.UI
{
    public class ScoreSystem : IScoreSystem
    {
        public ScoreSystem(SerializableGameSettings gameSettings)
        {
            var scoreList = gameSettings.ScoreList;
            foreach (var score in scoreList)
            {
                _scoreDictionary.Add(score.Key, score.Value);
            }
        }
        
        public event Action OnChanged = () => {};

        private readonly Dictionary<ETypeEnemy, int> _scoreDictionary = new ();
        
        public void AddScore(ETypeEnemy enemyType)
        {
            Score += _scoreDictionary[enemyType];
            OnChanged.Invoke();
        }

        public int Score { get; private set; }
    }
}

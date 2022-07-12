using TMPro;
using System;
using Game.UI.Interfaces;

namespace Game.UI
{
    public class ScoreView : IDisposable
    {
        public ScoreView(TextMeshProUGUI score, IScoreSystem scoreSystem)
        {
            _score = score;
            _scoreSystem = scoreSystem;
            _scoreSystem.OnChanged += UpdateText;
        }

        private readonly IScoreSystem _scoreSystem;
        private readonly TextMeshProUGUI _score;

        private void UpdateText()
        {
            _score.text = $"Score {_scoreSystem.Score}";
        }

        public void Dispose()
        {
            _scoreSystem.OnChanged -= UpdateText;
        }
    }
}
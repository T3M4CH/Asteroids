using Game.UI.Interfaces;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private IScoreSystem _scoreSystem;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _scoreSystem.AddScore(2);
        }
    }
}

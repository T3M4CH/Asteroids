using Game.Options.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Settings
{
    public class SceneSettings
    {
        public SceneSettings(ISceneSettings sceneSettings)
        {
            Application.targetFrameRate = sceneSettings.FPSRate;
        }
    }
}

using Game.Options.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    public class SceneSettings
    {
        public SceneSettings(int fpsRate)
        {
            Application.targetFrameRate = fpsRate;
        }
    }
}

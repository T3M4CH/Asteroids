using Game.Settings.Interfaces;
using Game.Settings.Enums;

namespace Game.Settings
{
    public class InputSettings : IInputSettings
    {
        public EInputScheme EInputScheme
        {
            get;
            set;
        }
    }
}

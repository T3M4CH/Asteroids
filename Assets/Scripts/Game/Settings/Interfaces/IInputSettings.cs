using Game.Settings.Enums;

namespace Game.Settings.Interfaces
{
    public interface IInputSettings
    {
        InputScheme InputScheme
        {
            get;
            set;
        }
    }
}

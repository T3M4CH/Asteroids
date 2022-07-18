using Game.Settings.Enums;

namespace Game.Settings.Interfaces
{
    public interface IInputSettings
    {
        EInputScheme EInputScheme
        {
            get;
            set;
        }
    }
}

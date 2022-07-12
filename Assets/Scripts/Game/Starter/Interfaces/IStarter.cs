using System;

namespace Game.Starter.Interfaces
{
    public interface IStarter
    {
        Action OnGameStart
        {
            get;
            set;
        }
    }
}

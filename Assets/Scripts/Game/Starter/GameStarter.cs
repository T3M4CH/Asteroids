using Game.Starter.Interfaces;
using System;

namespace Game.Starter
{
    public class GameStarter : IStarter
    {
        public Action OnGameStart
        {
            get;
            set;
        }
    }
}

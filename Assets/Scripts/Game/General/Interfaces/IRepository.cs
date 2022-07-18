using System.Collections.Generic;

namespace Game.General.Interfaces
{
    public interface IRepository<T>
    {
        List<T> List
        {
            get;
        }
    }
}

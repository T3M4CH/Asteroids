using System.Collections.Generic;

namespace Game.General
{
    public interface IRepository<T>
    {
        List<T> List
        {
            get;
        }
    }
}

using MikeDemoDBEntities.Models;
using System.Collections.Generic;

namespace MikeDemoDBEntities.Interfaces
{
    public interface IAutomobileRepository<T> where T : IAutomobileEntity
    {
        void AddAutomobile(T automobile);
        void RemoveAutmobile(int id);
        IEnumerable<Automobiles> GetAutomobiles();
        Automobiles FindById(int id);
    }
}

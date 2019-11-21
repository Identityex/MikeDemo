using MikeDemoDBEntities.Models;
using System.Collections.Generic;

namespace MikeDemoDBEntities.Interfaces
{
    public interface IAutomobileRepository<T> where T : AutomobileEntity
    {
        void AddAutomobile(T automobile, string type);
        void RemoveAutmobile(int id);
        IEnumerable<T> GetAutomobiles();
        Automobiles FindById(int id);
    }
}

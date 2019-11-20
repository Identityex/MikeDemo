using MikeDemoDBEntities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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

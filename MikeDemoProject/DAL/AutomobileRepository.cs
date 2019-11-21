using Microsoft.EntityFrameworkCore;
using MikeDemoDBEntities.Interfaces;
using MikeDemoDBEntities.Models;
using System.Collections.Generic;
using System.Linq;

namespace MikeDemoProject.DAL
{
    public class AutomobileRepository : IAutomobileRepository<Automobiles>
    {
        //DI for database
        private static MikeDBContext _mikeDBContext;
        public AutomobileRepository(MikeDBContext mikeDBContext)
        {
            _mikeDBContext = mikeDBContext;
        }

        /// <summary>
        /// Add Automobile to database when not using the Vehicle objects
        /// </summary>
        /// <param name="automobile">Automobile Entity object</param>
        public void AddAutomobile(Automobiles automobile, string type)
        {
            if (automobile != null)
            {
                automobile.AutomobileTypes = _mikeDBContext.AutomobileTypes
                    .FirstOrDefault(c => c.type == type);

                _mikeDBContext.Add(automobile);
                _mikeDBContext.SaveChanges();
            }
        }

        /// <summary>
        /// Find Automobile base on id
        /// </summary>
        /// <param name="id">Automobile id</param>
        /// <returns>Autmobile</returns>
        public Automobiles FindById(int id)
        {
            return _mikeDBContext.Automobiles
                .FirstOrDefault(c => c.AutomobileId == id);
        }

        /// <summary>
        /// Returns a listing of Autmobiles in database
        /// </summary>
        /// <returns>Listing of Automobiles</returns>
        public IEnumerable<Automobiles> GetAutomobiles()
        {
            return _mikeDBContext.Automobiles
                .Include(c => c.AutomobileTypes)
                .Select(c => c)
                .ToList();
        }

        /// <summary>
        /// Removes the requested Automobile
        /// </summary>
        /// <param name="id">id to remove</param>
        public void RemoveAutmobile(int id)
        {
            Automobiles automobile = _mikeDBContext.Automobiles
                .FirstOrDefault(c => c.AutomobileId == id);
            if (automobile != null)
            {
                _mikeDBContext.Remove(automobile);
                _mikeDBContext.SaveChanges();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MikeDemoDBEntities.Interfaces;
using MikeDemoDBEntities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeDemoProject.Models
{
    public class AutomobileRepoModel : IAutomobileRepository<Automobiles>
    {
        //DI for database
        private static MikeDBContext _mikeDBContext;
        public AutomobileRepoModel(MikeDBContext mikeDBContext)
        {
            _mikeDBContext = mikeDBContext;
        }

        /// <summary>
        /// Add Automobile to database when not using the Vehicle objects
        /// </summary>
        /// <param name="automobile">Automobile Entity object</param>
        public void AddAutomobile(Automobiles automobile)
        {
            if (automobile != null)
            {
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

        /// <summary>
        /// Takes a vehicle and adds it correctly to the database
        /// </summary>
        /// <typeparam name="T">Any class model that inherits the vehicle model</typeparam>
        /// <param name="vehicle">The input vehicle</param>
        public void AddVehicle<T>(T vehicle) where T : Vehicle
        {
            Automobiles automobile = null;
            if (typeof(T) == typeof(Truck))
            {
                Truck truck = (Truck)(object)vehicle;
                automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    AutomobileTypes = _mikeDBContext.AutomobileTypes
                        .FirstOrDefault(c => c.type == "Truck"),
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { truck.boxSize, truck.fourByFour })
                };
            }
            else if (typeof(T) == typeof(Car))
            {
                Car car = (Car)(object)vehicle;
                automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    AutomobileTypes = _mikeDBContext.AutomobileTypes
                        .FirstOrDefault(c => c.type == "Car"),
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { car.doors })
                };
            }
            else if (typeof(T) == typeof(Motorbike))
            {
                Motorbike motorbike = (Motorbike)(object)vehicle;
                automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    AutomobileTypes = _mikeDBContext.AutomobileTypes
                        .FirstOrDefault(c => c.type == "Motorbike"),
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { motorbike.passengerSeat })
                };
            }
            else
            {
                throw new Exception("Model not yet implemented");
            }

            AddAutomobile(automobile);
        }
    }
}

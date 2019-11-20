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

        private static MikeDBContext _mikeDBContext;
        public AutomobileRepoModel(MikeDBContext mikeDBContext)
        {
            _mikeDBContext = mikeDBContext;
        }

        public void AddAutomobile(Automobiles automobile)
        {
            if (automobile != null)
            {
                _mikeDBContext.Add(automobile);
                _mikeDBContext.SaveChanges();
            }
        }

        public Automobiles FindById(int id)
        {
            return _mikeDBContext.Automobiles
                .FirstOrDefault(c => c.AutomobileId == id);
        }

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
            if (typeof(T) == typeof(Truck))
            {
                Truck truck = (Truck)(object)vehicle;
                Automobiles automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    AutomobileTypes = _mikeDBContext.AutomobileTypes
                        .FirstOrDefault(c => c.type == "Truck"),
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { truck.boxSize, truck.fourByFour })
                };

                AddAutomobile(automobile);
            }
            else if (typeof(T) == typeof(Car))
            {
                Car car = (Car)(object)vehicle;
                Automobiles automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    AutomobileTypes = _mikeDBContext.AutomobileTypes
                        .FirstOrDefault(c => c.type == "Car"),
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { car.doors })
                };

                AddAutomobile(automobile);
            }
            else if (typeof(T) == typeof(Motorbike))
            {
                Motorbike motorbike = (Motorbike)(object)vehicle;
                Automobiles automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    AutomobileTypes = _mikeDBContext.AutomobileTypes
                        .FirstOrDefault(c => c.type == "Motorbike"),
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { motorbike.passengerSeat })
                };

                AddAutomobile(automobile);
            }
            else
            {
                throw new Exception("Model not yet implemented");
            }
        }
    }
}

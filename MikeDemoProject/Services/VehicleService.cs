using MikeDemoDBEntities.Models;
using MikeDemoProject.DAL;
using MikeDemoProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeDemoProject.Services
{
    public class VehicleService
    {
        private readonly AutomobileRepository _automobileRepository;
        public VehicleService(AutomobileRepository automobileRepository)
        {
            _automobileRepository = automobileRepository;
        }

        public enum VehicleTypes
        {
            Truck,
            Car,
            Motorbike
        }

        /// <summary>
        /// Takes a vehicle and adds it correctly to the database
        /// </summary>
        /// <typeparam name="T">Any class model that inherits the vehicle model</typeparam>
        /// <param name="vehicle">The input vehicle</param>
        public void AddVehicle<T>(T vehicle) where T : Vehicle
        {
            Automobiles automobile = null;
            string type = "";
            if (typeof(T) == typeof(Truck))
            {
                Truck truck = (Truck)(object)vehicle;
                automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { truck.boxSize, truck.fourByFour })
                };
                type = "Truck";
            }
            else if (typeof(T) == typeof(Car))
            {
                Car car = (Car)(object)vehicle;
                automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { car.doors })
                };
                type = "Car";
            }
            else if (typeof(T) == typeof(Motorbike))
            {
                Motorbike motorbike = (Motorbike)(object)vehicle;
                automobile = new Automobiles
                {
                    Colour = vehicle.colour,
                    DateAdded = DateTime.Now,
                    Price = vehicle.price,
                    Name = vehicle.name,
                    JsonDetails = JsonConvert.SerializeObject(new { motorbike.passengerSeat })
                };
                type = "Motorbike";
            }
            else
            {
                throw new Exception("Model not yet implemented");
            }

            _automobileRepository.AddAutomobile(automobile, type);
        }

        public void RemoveVehicleById(int id)
        {
            _automobileRepository.RemoveAutmobile(id);
        }

        public T GetVehicleByName<T>(string name)
        {
            throw new NotImplementedException();
        }

        public T GetVehicleById<T>(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all Vehicles of a particular type
        /// </summary>
        /// <typeparam name="T">Vehicle class Type</typeparam>
        /// <param name="type">enum of vehicle type</param>
        /// <returns>a list of typed vehicles</returns>
        public List<T> GetAllByType<T>(VehicleTypes type)
        {
            IEnumerable<Automobiles> automobiles = _automobileRepository.GetAutomobiles();
            if (type == VehicleTypes.Truck)
            {
                return (List<T>)(object)automobiles
                    .Select(c => new Truck
                    {
                        vehicleId = c.AutomobileId,
                        colour = c.Colour,
                        name = c.Name,
                        price = c.Price,
                        boxSize = JsonConvert.DeserializeObject<Truck>(c.JsonDetails).boxSize,
                        fourByFour = JsonConvert.DeserializeObject<Truck>(c.JsonDetails).fourByFour
                    })
                    .ToList();
            }
            else if (type == VehicleTypes.Car)
            {
                return (List<T>)(object)automobiles
                    .Select(c => new Car
                    {
                        vehicleId = c.AutomobileId,
                        colour = c.Colour,
                        name = c.Name,
                        price = c.Price,
                        doors = JsonConvert.DeserializeObject<Car>(c.JsonDetails).doors
                    })
                    .ToList();
            }
            else if (type == VehicleTypes.Motorbike)
            {
                return (List<T>)(object)automobiles
                    .Select(c => new Motorbike
                    {
                        vehicleId = c.AutomobileId,
                        colour = c.Colour,
                        name = c.Name,
                        price = c.Price,
                        passengerSeat = JsonConvert.DeserializeObject<Motorbike>(c.JsonDetails).passengerSeat
                    })
                    .ToList();
            }
            else
            {
                throw new Exception("What are you doing here?");
            }
        }

        /// <summary>
        /// Get All vehicles of a base type
        /// </summary>
        /// <returns>All vehicles of a base type</returns>
        public List<Vehicle> GetAllVehicles()
        {
            IEnumerable<Automobiles> automobiles = _automobileRepository.GetAutomobiles();

            return automobiles.Select(c => new Vehicle
            {
                vehicleId = c.AutomobileId,
                colour = c.Colour,
                name = c.Name,
                price = c.Price
            })
            .ToList();
        }
    }
}

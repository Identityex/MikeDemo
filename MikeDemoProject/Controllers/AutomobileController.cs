using Microsoft.AspNetCore.Mvc;
using MikeDemoProject.Models;
using MikeDemoProject.Services;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MikeDemoProject.Controllers
{
    public class AutomobileController : Controller
    {
        private readonly VehicleService _vehicleService;

        //Setup class constructor for dependency injection
        public AutomobileController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            //Like doing it this way makes pages more managable when coding
            return View("Automobile");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicle(string vehicle, string type)
        {
            try
            {
                switch (type)
                {
                    //Deserialize object myself since it can be multiple cases
                    case "Truck":
                        Truck truck = JsonConvert.DeserializeObject<Truck>(vehicle);
                        _vehicleService.AddVehicle(truck);
                        break;
                    case "Car":
                        Car car = JsonConvert.DeserializeObject<Car>(vehicle);
                        _vehicleService.AddVehicle(car);
                        break;
                    case "Motorbike":
                        Motorbike motorbike = JsonConvert.DeserializeObject<Motorbike>(vehicle);
                        _vehicleService.AddVehicle(motorbike);
                        break;
                    default:
                        return BadRequest("Not Accepted Type");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred :" + ex.Message);
            }
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public void DeleteVehicle(int id)
        {
            _vehicleService.RemoveVehicleById(id);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public JsonResult GetVehicles()
        {
            var returnList = _vehicleService.GetAllVehicles()
                .Select(c => new { c.vehicleId, c.colour, c.price, c.name });
            return Json(returnList);
        }
    }
}
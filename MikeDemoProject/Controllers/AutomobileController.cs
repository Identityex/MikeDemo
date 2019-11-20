using Microsoft.AspNetCore.Mvc;
using MikeDemoProject.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MikeDemoProject.Controllers
{
    public class AutomobileController : Controller
    {
        private readonly AutomobileRepoModel _automobileRepoModel;

        //Setup class constructor for dependency injection
        public AutomobileController(AutomobileRepoModel automobileRepoModel)
        {
            _automobileRepoModel = automobileRepoModel;
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
                        _automobileRepoModel.AddVehicle(truck);
                        break;
                    case "Car":
                        Car car = JsonConvert.DeserializeObject<Car>(vehicle);
                        _automobileRepoModel.AddVehicle(car);
                        break;
                    case "Motorbike":
                        Motorbike motorbike = JsonConvert.DeserializeObject<Motorbike>(vehicle);
                        _automobileRepoModel.AddVehicle(motorbike);
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
            _automobileRepoModel.RemoveAutmobile(id);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public JsonResult GetVehicles()
        {
            var returnList = _automobileRepoModel.GetAutomobiles()
                .Select(c => new { c.AutomobileId, c.Colour, c.Price, c.Name });
            return Json(returnList);
        }
    }
}
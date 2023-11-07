using Cros4Classes;
using Cros5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cros5.Controllers
{
    [Authorize]
    public class LabsController : Controller
    {      
        [HttpPost]
        public IActionResult Calculate(LabModel model)
        {
            try
            {
                model.Output = LabExecutor.Run(model.LabNumber, model.Input);

                string labView = "";
                switch (model.LabNumber)
                {
                    case 1:
                        labView = "Lab1";
                        break;
                    case 2:
                        labView = "Lab2";
                        break;
                    case 3:
                        labView = "Lab3";
                        break;
                }
                return View(labView, model);
            }
            catch (Exception e)
            {
                model.Output = $"Error: {e.Message}";

                string labView = $"Lab{model.LabNumber}";
                return View(labView, model);
            }
        }

        public IActionResult Lab1()
        {
            return View();
        }

        public IActionResult Lab2()
        {
            return View();
        }

        public IActionResult Lab3()
        {
            return View();
        }
    }
}

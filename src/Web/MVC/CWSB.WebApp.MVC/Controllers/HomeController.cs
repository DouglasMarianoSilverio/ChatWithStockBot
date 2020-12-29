using CWSB.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var errorModel = new ErrorViewModel();

            if (id == 500)
            {
                errorModel.Message = "An error has occured! Please try again later or contact the support.";
                errorModel.Title = "An error has occured!";
                errorModel.ErrorCode = id;
            }
            else if (id == 404)
            {
                errorModel.Message =
                    "the page you were looking for cannot be found!<br />Please contact support for assistance."; 
                errorModel.Title = "Ops! Page not found.";
                errorModel.ErrorCode = id;
            }
            else if (id == 403)
            {
                errorModel.Message = "You don't have permission for this action.";
                errorModel.Title = "Access denied";
                errorModel.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", errorModel);
        }
    }
}

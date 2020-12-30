using CWSB.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Controllers
{
    public class ChatController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PostMessage(PostCreateRequest postMessage)
        {
            if (!ModelState.IsValid) return View(postMessage);

            return View();


        }

    }
}

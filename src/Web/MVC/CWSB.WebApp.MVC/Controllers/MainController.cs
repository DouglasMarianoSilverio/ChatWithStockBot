using CWSB.Core.Communications;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CWSB.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if( response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }
            return false;
        }
    }
}

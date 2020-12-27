using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Controllers
{
    
    [ApiController]    
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result=null)
        {
            if (ValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages",Errors.ToArray()}
            })) ;
        }

        //todo - Add function to handle fail fast validations.

        private bool ValidOperation()
        {
            return !(Errors.Any());
        }

        //protected void AddModelValidationErrors(List<>)

        protected void AddOperationError(string error)
        {
            Errors.Add(error);
        }

        protected void ClearOperationErrors()
        {
            Errors.Clear();
        }




    }
}

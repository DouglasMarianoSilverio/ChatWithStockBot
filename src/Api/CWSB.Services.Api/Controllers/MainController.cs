using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddOperationError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        private bool ValidOperation()
        {
            return !(Errors.Any());
        }

        

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

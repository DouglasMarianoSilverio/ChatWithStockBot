using CWSB.Core.Communications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CWSB.Core.Models
{
    
    public class PostCreateRequest
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(255, ErrorMessage = "Field {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Message { get; set; }        
    }
    
    public class PostCreateResponse
    {
        public bool Succeeded { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace CWSB.Services.Api.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(255, ErrorMessage = "Field {0} must be between {2} and {1} characters.", MinimumLength = 2)]  
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }

        public Post()
        { 
        }

        public Post(string text, string user)
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            User = user;
            Text = text;
        }
    }
}

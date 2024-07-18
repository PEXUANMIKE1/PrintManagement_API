using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.RequestModels.UserRequests
{
    public class Request_Register
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; } = string.Empty;
        //[Required(ErrorMessage = "TeamId is required")]
        //public int TeamId { get; set; }
    }
}

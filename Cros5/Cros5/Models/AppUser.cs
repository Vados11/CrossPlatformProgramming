using Microsoft.AspNetCore.Mvc;

namespace Cros5.Models
{
    public class AppUser
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}

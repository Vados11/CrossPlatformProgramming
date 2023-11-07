using Newtonsoft.Json;

namespace Cros5.Models
{
    public class OktaUser
    {
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public string Email { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}

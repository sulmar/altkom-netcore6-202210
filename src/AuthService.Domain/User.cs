using Altkom.Net6.Domain;

namespace AuthService.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; } // hash+solenie
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
    }
}
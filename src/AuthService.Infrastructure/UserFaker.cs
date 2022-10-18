using AuthService.Domain;
using Bogus;

namespace AuthService.Infrastructure
{
    public class UserFaker : Faker<User>
    {
        public UserFaker()
        {
            UseSeed(0);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Username, f => f.Person.UserName);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f=>f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.Phone, f => f.Phone.PhoneNumber());
            RuleFor(p => p.Birthday, f => f.Date.Past(40));

            // TODO: zahashować!
            RuleFor(p => p.HashedPassword, f => "12345");
        }
    }
}
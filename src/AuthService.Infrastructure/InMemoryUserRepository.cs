using Altkom.Net6.Domain;
using Altkom.Net6.Infrastructure;
using AuthService.Domain;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure
{

    public class InMemoryUserRepository : InMemoryEntityRepository<User>, IUserRepository
    {
        // dotnet add package Microsoft.Extensions.Identity.Core
        public InMemoryUserRepository(IPasswordHasher<User> passwordHasher)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username="john", FirstName = "John", LastName = "Smith", Email="john@domain.com", Phone = "555-000-000" },
                new User { Id = 2, Username="kate", FirstName = "Kate", LastName = "Smith", Email="kate@domain.com", Phone = "555-111-000" },
                new User { Id = 3, Username="bob",  FirstName = "Bob",  LastName = "Smith", Email="bob@domain.com", Phone = "555-222-000" },
            };

            foreach (var user  in users)
            {
                user.HashedPassword = passwordHasher.HashPassword(user, "123");
            }

            _entities = users.ToDictionary(p => p.Id); // Primary Key ;-)
        }
        
        public User GetByUsername(string username)
        {
            return _entities.Values.SingleOrDefault(e => e.Username == username);
        }
    }
}
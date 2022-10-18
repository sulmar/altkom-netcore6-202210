using AuthService.Domain;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure
{
    public class MyAuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public MyAuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public bool TryAuthorize(string username, string password, out User user)
        {
            user = userRepository.GetByUsername(username);

            return user != null && passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password) == PasswordVerificationResult.Success;
        }

       
    }
}
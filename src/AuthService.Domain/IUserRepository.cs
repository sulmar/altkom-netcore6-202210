using Altkom.Net6.Domain;

namespace AuthService.Domain
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetByUsername(string username);
    }
}
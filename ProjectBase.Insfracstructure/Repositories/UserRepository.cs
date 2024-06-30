using ProjectBase.Domain.Data;
using ProjectBase.Domain.Entities;
using ProjectBase.Domain.Repositories;

namespace ProjectBase.Insfracstructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDBContext context) : base(context)
        {

        }
    }
}

using ProjectBase.Domain.Repositories;
using ProjectBase.Domain.Data;
using ProjectBase.Insfracstructure.Repositories;

namespace ProjectBase.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDBContext _context;
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }

        public IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

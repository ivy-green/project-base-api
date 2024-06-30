
using ProjectBase.Domain.Repositories;

namespace ProjectBase.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}

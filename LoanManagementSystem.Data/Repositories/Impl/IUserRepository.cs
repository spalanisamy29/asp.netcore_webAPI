using LoanManagementSystem.Data.Context;
using LoanManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }

   
}
using LoanManagementSystem.Service.DTOs.RequestDTOs;
using LoanManagementSystem.Service.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Service.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUserAsync(UserRegistrationRequestDto dto, CancellationToken cancellationToken);
        Task<UserResponseDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task UpdateUserAsync(int id, UserRegistrationRequestDto dto, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    }
}

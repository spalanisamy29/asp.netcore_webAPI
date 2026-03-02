using LoanManagementSystem.Data.Context;
using LoanManagementSystem.Data.Entities;
using LoanManagementSystem.Data.Repositories.Interface;
using LoanManagementSystem.Service.DTOs.RequestDTOs;
using LoanManagementSystem.Service.DTOs.ResponseDTOs;
using LoanManagementSystem.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly LMSDbContext _context;

       
        public UserService(LMSDbContext context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;

        }

        public async Task<int> RegisterUserAsync(UserRegistrationRequestDto dto, CancellationToken cancellationToken)
        {
            var exists = await _context.Users
                .AnyAsync(x => x.Email == dto.Email, cancellationToken);

            if (exists)
                throw new Exception("Email already registered.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                NativePlace = dto.NativePlace,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.UserId;
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null) return null;

            return new UserResponseDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToList();
        }

        public async Task UpdateUserAsync(int id, UserRegistrationRequestDto dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.NativePlace = dto.NativePlace;       
            user.PasswordHash = dto.Password; // hash in production
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _userRepository.DeleteAsync(id, cancellationToken);
        }
    }
}

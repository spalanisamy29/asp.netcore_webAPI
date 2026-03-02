using LoanManagementSystem.Service.DTOs.RequestDTOs;
using LoanManagementSystem.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequestDto dto,CancellationToken cancellationToken)
        {
            var id = await _userService.RegisterUserAsync(dto, cancellationToken);
            return Ok(id);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);
            if (user == null)
                return NotFound(new ProblemDetails { Title = "User not found", Status = 404 });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync(cancellationToken);
            return Ok(users);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRegistrationRequestDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.UpdateUserAsync(id, dto, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails { Title = "User not found", Status = 404 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteUserAsync(id, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails { Title = "User not found", Status = 404 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}

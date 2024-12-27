using Microsoft.AspNetCore.Mvc;
using YetkiSistemi.Core.Entities;
using YetkiSistemi.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [PermissionCheck("CanList")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [PermissionCheck("CanCreate")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveAsync();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
        if (existingUser == null) return NotFound();

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        existingUser.IsAdmin = user.IsAdmin;
        existingUser.PermissionId = user.PermissionId;

        await _unitOfWork.SaveAsync();
        return Ok(existingUser);
    }

    [HttpDelete("{id}")]
    [PermissionCheck("CanDelete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();

        _unitOfWork.Users.Remove(user);
        await _unitOfWork.SaveAsync();
        return Ok("User deleted successfully.");
    }
}

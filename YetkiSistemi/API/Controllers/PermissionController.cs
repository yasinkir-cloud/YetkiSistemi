using Microsoft.AspNetCore.Mvc;
using YetkiSistemi.Core.Entities;
using YetkiSistemi.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PermissionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [PermissionCheck("CanList")]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _unitOfWork.Permissions.GetAllAsync();
        return Ok(permissions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermissionById(int id)
    {
        var permission = await _unitOfWork.Permissions.GetByIdAsync(id);
        if (permission == null) return NotFound();
        return Ok(permission);
    }

    [HttpPost]
    [PermissionCheck("CanCreate")]
    public async Task<IActionResult> AddPermission([FromBody] Permission permission)
    {
        await _unitOfWork.Permissions.AddAsync(permission);
        await _unitOfWork.SaveAsync();
        return Ok(permission);
    }

    [HttpPut("{id}")]
    [PermissionCheck("CanUpdate")]
    public async Task<IActionResult> UpdatePermission(int id, [FromBody] Permission permission)
    {
        var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(id);
        if (existingPermission == null) return NotFound();

        existingPermission.Name = permission.Name;
        await _unitOfWork.SaveAsync();

        return Ok(existingPermission);
    }

    [HttpDelete("{id}")]
    [PermissionCheck("CanDelete")]
    public async Task<IActionResult> DeletePermission(int id)
    {
      
        var permission = await _unitOfWork.Permissions.GetByIdAsync(id);
        if (permission == null) return NotFound();

        _unitOfWork.Permissions.Remove(permission);
        await _unitOfWork.SaveAsync();

        return Ok("Permission deleted successfully.");
    }
}

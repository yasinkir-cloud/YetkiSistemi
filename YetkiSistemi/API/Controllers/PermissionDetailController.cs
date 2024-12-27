using Microsoft.AspNetCore.Mvc;
using YetkiSistemi.Core.Entities;
using YetkiSistemi.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PermissionDetailController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PermissionDetailController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [PermissionCheck("CanList")]
    public async Task<IActionResult> GetAllPermissionDetails()
    {
        var permissionDetails = await _unitOfWork.PermissionDetails.GetAllAsync();
        return Ok(permissionDetails);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermissionDetailById(int id)
    {
        var permissionDetail = await _unitOfWork.PermissionDetails.GetByIdAsync(id);
        if (permissionDetail == null) return NotFound();
        return Ok(permissionDetail);
    }

    [HttpPost]
    [PermissionCheck("CanCreate")]
    public async Task<IActionResult> AddPermissionDetail([FromBody] PermissionDetail permissionDetail)
    {
        await _unitOfWork.PermissionDetails.AddAsync(permissionDetail);
        await _unitOfWork.SaveAsync();
        return Ok(permissionDetail);
    }

    [HttpPut("{id}")]
    [PermissionCheck("CanUpdate")]
    public async Task<IActionResult> UpdatePermissionDetail(int id, [FromBody] PermissionDetail permissionDetail)
    {
        var existingDetail = await _unitOfWork.PermissionDetails.GetByIdAsync(id);
        if (existingDetail == null) return NotFound();

        existingDetail.PermissionId = permissionDetail.PermissionId;
        existingDetail.PageId = permissionDetail.PageId;
        existingDetail.CanList = permissionDetail.CanList;
        existingDetail.CanCreate = permissionDetail.CanCreate;
        existingDetail.CanUpdate = permissionDetail.CanUpdate;
        existingDetail.CanDelete = permissionDetail.CanDelete;

        await _unitOfWork.SaveAsync();
        return Ok(existingDetail);
    }

    [HttpDelete("{id}")]
    [PermissionCheck("CanDelete")]
    public async Task<IActionResult> DeletePermissionDetail(int id)
    {
        var permissionDetail = await _unitOfWork.PermissionDetails.GetByIdAsync(id);
        if (permissionDetail == null) return NotFound();

        _unitOfWork.PermissionDetails.Remove(permissionDetail);
        await _unitOfWork.SaveAsync();

        return Ok("PermissionDetail deleted successfully.");
    }
}

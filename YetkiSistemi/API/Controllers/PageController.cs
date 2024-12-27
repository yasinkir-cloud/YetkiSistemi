using Microsoft.AspNetCore.Mvc;
using YetkiSistemi.Core.Entities;
using YetkiSistemi.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PageController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
   
    public PageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [PermissionCheck("CanList")]
    public async Task<IActionResult> GetAllPages()
    {
        var pages = await _unitOfWork.Pages.GetAllAsync();
        return Ok(pages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPageById(int id)
    {
        var page = await _unitOfWork.Pages.GetByIdAsync(id);
        if (page == null) return NotFound();
        return Ok(page);
    }

    [HttpPost]
    [PermissionCheck("CanCreate")]
    public async Task<IActionResult> AddPage([FromBody] Page page)
    {
        await _unitOfWork.Pages.AddAsync(page);
        await _unitOfWork.SaveAsync();
        return Ok(page);
    }

    [HttpPut("{id}")]
    [PermissionCheck("CanUpdate")]
    public async Task<IActionResult> UpdatePage(int id, [FromBody] Page page)
    {
        var existingPage = await _unitOfWork.Pages.GetByIdAsync(id);
        if (existingPage == null) return NotFound();

        existingPage.Code = page.Code;
        await _unitOfWork.SaveAsync();

        return Ok(existingPage);
    }

    [HttpDelete("{id}")]
    [PermissionCheck("CanDelete")]
    public async Task<IActionResult> DeletePage(int id)
    {
        var page = await _unitOfWork.Pages.GetByIdAsync(id);
        if (page == null) return NotFound();

        _unitOfWork.Pages.Remove(page);
        await _unitOfWork.SaveAsync();

        return Ok("Page deleted successfully.");
    }
}

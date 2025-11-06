using Microsoft.AspNetCore.Mvc;
using StoreService.API.Models.DTOs;
using StoreService.API.Services;

namespace StoreService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    public ReportsController(IReportService reportService) => _reportService = reportService;

    [HttpGet("birthdays")]
    public async Task<IActionResult> GetBirthdays([FromQuery] DateTime date, CancellationToken ct)
    {
        var result = await _reportService.GetBirthdayPeopleAsync(date, ct);

        return Ok(result);
    }

    [HttpGet("recent-buyers")]
    public async Task<IActionResult> GetRecentBuyers([FromQuery] int days, CancellationToken ct)
    {
        if (days <= 0) return BadRequest("Days must be positive");

        var result = await _reportService.GetBuyersByDaysAsync(days, ct);

        return Ok(result);
    }

    [HttpGet("popular-categories/{customerId}")]
    public async Task<ActionResult<List<CategoryStatDto>>> GetPopularCategories(int customerId, CancellationToken ct)
    {
        if (customerId <= 0) return BadRequest("CustomerId must be positive");

        var result = await _reportService.GetPopularCategoriesAsync(customerId, ct);

        if (result == null || !result.Any()) return NotFound();

        return Ok(result);
    }

}

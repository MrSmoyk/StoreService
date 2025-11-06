using Microsoft.EntityFrameworkCore;
using StoreService.API.Data;
using StoreService.API.Models.DTOs;

namespace StoreService.API.Services;

public class ReportService : IReportService
{
    private readonly StoreContext _context;
    public ReportService(StoreContext db) => _context = db;

    public async Task<List<CategoryStatDto>> GetPopularCategoriesAsync(int customerId, CancellationToken ct = default)
    {
        var result = await _context.PurchaseItems
            .Where(pi => pi.Purchase != null && pi.Purchase.CustomerId == customerId)
            .Include(pi => pi.Purchase)
            .Include(pi => pi.Product)
            .GroupBy(pi => pi.Product.Category)
            .Select(g => new CategoryStatDto
            {
                Category = g.Key.ToString(),
                TotalUnits = g.Sum(pi => pi.Quantity)
            })
            .ToListAsync(ct);

        return result;
    }

    public async Task<List<RecentBuyersDTO>> GetBuyersByDaysAsync(int days, CancellationToken ct = default)
    {
        var since = DateTime.UtcNow.AddDays(-days);

        var result = await _context.Purchases
            .Where(p => p.Date >= since)
            .GroupBy(p => p.Customer)
            .Select(g => new RecentBuyersDTO
            {
                CustomerId = g.Key.Id,
                CustomerFullName = g.Key.FullName,
                LastPurchase = g.Max(p => p.Date)
            })
            .ToListAsync(ct);

        return result;
    }

    public async Task<List<BirthdaysDTO>> GetBirthdayPeopleAsync(DateTime date, CancellationToken ct = default)
    {
        var result = await _context.Customers
            .Where(c => c.BirthDate.Month == date.Month && c.BirthDate.Day == date.Day)
            .Select(c => new BirthdaysDTO
            {
                CustomerId = c.Id,
                CustomerFullName = c.FullName
            })
            .ToListAsync(ct);

        return result;
    }
}

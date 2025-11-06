using StoreService.API.Models.DTOs;

namespace StoreService.API.Services;

public interface IReportService
{
    Task<List<CategoryStatDto>> GetPopularCategoriesAsync(int customerId, CancellationToken ct = default);
    Task<List<RecentBuyersDTO>> GetBuyersByDaysAsync(int days, CancellationToken ct = default);
    Task<List<BirthdaysDTO>> GetBirthdayPeopleAsync(DateTime date, CancellationToken ct = default);
}

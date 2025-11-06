using System.ComponentModel.DataAnnotations;

namespace StoreService.API.Models.DTOs;

public class RecentBuyersDTO
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public string CustomerFullName { get; set; }

    public DateTime LastPurchase { get; set; }
}

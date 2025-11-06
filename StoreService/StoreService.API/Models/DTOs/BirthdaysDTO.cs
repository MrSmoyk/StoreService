using System.ComponentModel.DataAnnotations;

namespace StoreService.API.Models.DTOs;

public class BirthdaysDTO
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public string CustomerFullName { get; set; }
}

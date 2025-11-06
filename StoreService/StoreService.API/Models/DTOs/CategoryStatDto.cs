using StoreService.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoreService.API.Models.DTOs;

public class CategoryStatDto
{
    [Required]
    public string Category { get; set; }
    [Required]
    public int TotalUnits { get; set; }
}

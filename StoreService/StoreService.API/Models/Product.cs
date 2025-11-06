using StoreService.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoreService.API.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; }

    [Required]
    public Category Category { get; set; }

    [Required, MaxLength(100)]
    public string SKU { get; set; }

    [Required]
    public decimal Price { get; set; }
}

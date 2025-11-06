namespace StoreService.API.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}

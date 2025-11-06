namespace StoreService.API.Models;

public class Purchase
{
    public int Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
}

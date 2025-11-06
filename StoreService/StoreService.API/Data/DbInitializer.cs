using Microsoft.EntityFrameworkCore;
using StoreService.API.Models;
using StoreService.API.Models.Enums;

namespace StoreService.API.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(StoreContext context)
    {
        if (await context.Customers.AnyAsync())
            return;

        await using var tx = await context.Database.BeginTransactionAsync();

        try
        {
            var customers = new List<Customer>
            {
                new() { FullName = "Іван Петренко", BirthDate = new DateTime(1990, 11, 6), RegistrationDate = DateTime.UtcNow.AddMonths(-4) },
                new() { FullName = "Дмитро Пономаренко", BirthDate = new DateTime(1994, 11, 6), RegistrationDate = DateTime.UtcNow.AddMonths(-3) },
                new() { FullName = "Олена Коваленко", BirthDate = new DateTime(1985, 5, 10), RegistrationDate = DateTime.UtcNow.AddMonths(-2) },
                new() { FullName = "Петро Шевченко", BirthDate = new DateTime(1995, 12, 25), RegistrationDate = DateTime.UtcNow.AddMonths(-1) }
            };

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();

            var products = new List<Product>
            {
                new() { Name = "Ноутбук Lenovo", Category = Category.Electronics, SKU = "LN-001", Price = 35000 },
                new() { Name = "Мишка Logitech", Category = Category.Electronics, SKU = "LG-002", Price = 900 },
                new() { Name = "Книга C# Advanced", Category = Category.Books, SKU = "BK-003", Price = 700 },
                new() { Name = "Футболка Nike", Category = Category.Clothing, SKU = "CL-004", Price = 1200 },
                new() { Name = "Кросівки Adidas", Category = Category.Clothing, SKU = "CL-005", Price = 2500 }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();


            var purchases = new List<Purchase>
            {
                new()
                {
                    Number = "ORD-1001",
                    Date = DateTime.UtcNow.AddDays(-5),
                    CustomerId = customers[0].Id,
                    Customer = customers[0]
                },
                new()
                {
                    Number = "ORD-1002",
                    Date = DateTime.UtcNow.AddDays(-2),
                    CustomerId = customers[1].Id,
                    Customer = customers[1]
                },
                new()
                {
                    Number = "ORD-1003",
                    Date = DateTime.UtcNow.AddDays(-1),
                    CustomerId = customers[0].Id,
                    Customer = customers[0]
                }
            };

            await context.Purchases.AddRangeAsync(purchases);
            await context.SaveChangesAsync();

            var purchaseItems = new List<PurchaseItem>
            {
                new() { PurchaseId = purchases[0].Id, ProductId = products[0].Id, Quantity = 1 },
                new() { PurchaseId = purchases[0].Id, ProductId = products[1].Id, Quantity = 2 },
                new() { PurchaseId = purchases[1].Id, ProductId = products[2].Id, Quantity = 1 },
                new() { PurchaseId = purchases[1].Id, ProductId = products[3].Id, Quantity = 3 },
                new() { PurchaseId = purchases[2].Id, ProductId = products[4].Id, Quantity = 1 }
            };

            await context.PurchaseItems.AddRangeAsync(purchaseItems);
            await context.SaveChangesAsync();

            foreach (var purchase in purchases)
            {
                purchase.Total = context.PurchaseItems
                    .Where(i => i.PurchaseId == purchase.Id)
                    .Sum(i => i.Quantity * i.Product.Price);
            }

            await context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}
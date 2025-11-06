using Microsoft.EntityFrameworkCore;
using StoreService.API.Data;
using StoreService.API.Middleware;
using StoreService.API.Services;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=shop.db";
 builder.Services.AddDbContext<StoreContext>(opt =>
 opt.UseSqlite(conn));

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<StoreContext>();
    await ctx.Database.MigrateAsync();
    await DbInitializer.InitializeAsync(ctx);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();

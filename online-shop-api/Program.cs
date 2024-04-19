using online_shop_api.Config;
using online_shop_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<UserService>();

builder.Services.Configure<OnlineShopDatabaseSettings>(
    builder.Configuration.GetSection("OnlineShopDatabase"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

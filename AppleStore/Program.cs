using AppleStore.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);
string? deviceConnection = builder.Configuration.GetConnectionString("DeviceConnection");
string? orderConnection = builder.Configuration.GetConnectionString("OrderConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DeviceDbContext>(options => options.UseNpgsql(deviceConnection));
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(orderConnection));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<OrderRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Device}/{action=Catalog}/{id?}");

app.Run();
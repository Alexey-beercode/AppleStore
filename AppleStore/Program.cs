using LogLevel = NLog.LogLevel;

var builder = WebApplication.CreateBuilder(args);
string? deviceConnection = builder.Configuration.GetConnectionString("DeviceConnection");
string? orderConnection = builder.Configuration.GetConnectionString("OrderConnection");
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DeviceDbContext>(options => options.UseNpgsql(deviceConnection));
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(orderConnection));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<DeviceRepository>();
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

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
    pattern: "{controller=Device}/{action=Catalog}/{type=-1}/{id?}");

logger.Log(LogLevel.Info, "Инициализация программы");
app.Run();
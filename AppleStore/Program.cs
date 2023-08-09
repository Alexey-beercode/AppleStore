using LogLevel = NLog.LogLevel;

var builder = WebApplication.CreateBuilder(args);
string? deviceConnection = builder.Configuration.GetConnectionString("DeviceConnection");
string? orderConnection = builder.Configuration.GetConnectionString("OrderConnection");
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DeviceDbContext>(options => options.UseNpgsql(deviceConnection));
builder.Services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(orderConnection));

builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<DeviceRepository>();

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.Services.AddMemoryCache();

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();
logger.Log(LogLevel.Info, "Инициализация программы");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseResponseCompression();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Device}/{action=Catalog}/{type=-1}/{id?}");

app.Run();
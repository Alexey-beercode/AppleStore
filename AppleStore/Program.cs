using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using LogLevel = NLog.LogLevel;

var builder = WebApplication.CreateBuilder(args);
string? deviceConnection = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddControllersWithViews(x => x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(deviceConnection));
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<DeviceRepository>();

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie( options=>
    {
        options.Cookie.Name = "AppleStoreAuthentication";
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Error";
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(x=>x.AddPolicy("AdminArea",policy=>policy.RequireRole("Admin")));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute( "default","{controller=Device}/{action=Catalog}/{type=-1}/{id?}");
app.MapControllerRoute("admin", "{area=Admin}/{controller=Home}/{action=Index}/{id?}");
app.Run();
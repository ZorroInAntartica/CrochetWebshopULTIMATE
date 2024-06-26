using CrochetWebshop.DAL;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Repositories;
using CrochetWebshop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Configuring Session Services in ASP.NET Core
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.IOTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.Name = ".CrochetWebshop.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("Connection1");
builder.Services.AddDbContext<Connection1Context>(x => x.UseSqlite(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Connection1Context>()
    .AddDefaultTokenProviders();

// Register your repositories
builder.Services.AddScoped<iUserRepository, UserRepository>();
builder.Services.AddScoped<iOrderRepository, OrderRepository>();
builder.Services.AddScoped<iProductRepository, ProductRepository>();
builder.Services.AddScoped<iCustomerRepository, CustomerRepository>();

// Register your services
builder.Services.AddScoped<iUserService, UserService>();
builder.Services.AddScoped<iOrderService, OrderService>();
builder.Services.AddScoped<iProductService, ProductService>();
builder.Services.AddScoped<iCustomerService, CustomerService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreatorPolicy", policy => policy.RequireRole("Creator"));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
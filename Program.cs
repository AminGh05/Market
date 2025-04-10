using Market.Data;
using Market.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Db Context
builder.Services.AddDbContext<MarketContext>(options =>
{
	options.UseSqlServer("Data Source = AMINGH05; Initial Catalog = Market; Integrated Security = True; TrustServerCertificate = True");
});
#endregion

#region Repository
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
//builder.Services.AddTransient<IGroupRepository, GroupRepository>();
//builder.Services.AddSingleton<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/Login";
	options.LogoutPath = "/Logout";
	options.ExpireTimeSpan = TimeSpan.FromDays(14);
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

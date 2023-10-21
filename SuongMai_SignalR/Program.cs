using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuongMai_SignalR.Data;
using SuongMai_SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
// n?u sd azure signal R
//1. Dowload package: microsoft.azure.signalR
// var connection string = ""; find it in: connectiontring=> priarykey=> connection string
//builder.Services.AddSignalR().AddAzureSignalR(string);
// => build application, it will work on your azure , it will show data in overview screen in azure





builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapHub<UserHub>("/hubs/userCount");
app.MapHub<DeathlyHallowsHub>("/hubs/deathyhallows");
app.MapHub<HouseGroupHub>("/hubs/houseGroup");
app.MapHub<NotificationHub>("/hubs/notificationHub");
app.MapHub<BasicChatHub>("/hubs/basicChatHub");
app.MapHub<ChatHub>("/hubs/chat");





app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

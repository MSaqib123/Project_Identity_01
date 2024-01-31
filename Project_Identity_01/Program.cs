#region Configuartion,Middlware 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Identity_01.Data;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

//========================================
//-------- 1. SetUp DbConnection ---------
//========================================
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//========================================
//-------- 2. SetUp Identity ---------
//========================================
//AddIdentityCore  do not give roles , cookies acces
//AddIdentity is full access
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();
#endregion





#region Request Piplines

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//_____ 1. Add Authentication ____
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
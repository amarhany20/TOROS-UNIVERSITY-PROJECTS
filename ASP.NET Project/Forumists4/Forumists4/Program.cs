using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Forumists4.Data;
using Forumists4.Areas.Identity.Data;
using Forumists4.Data.Enums;

var builder = WebApplication.CreateBuilder(args);//My Builder that has all the services


// Add services to the container.
//All of my added codes

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//Getting the connection from the JSON File
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));// Using my connection string

builder.Services.AddDatabaseDeveloperPageExceptionFilter();//I am not sure but I think that is for errors in database

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)//Adding a default Identity (DONNOW WHY) And in here I can choose the option for confirmation or not)
    .AddRoles<IdentityRole>()//Adding the Roles of the Identity Roles
    .AddEntityFrameworkStores<ApplicationDbContext>();//Donnow

builder.Services.AddControllersWithViews();//This is the main MVC Service


builder.Services.AddRazorPages();//I think the Identity is Web Application so, it uses Razor PAGES

builder.Services.Configure<IdentityOptions>(options =>
{
    //options.Password.RequireDigit = true;
    //options.Password.RequiredLength = 5;

    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    //options.Lockout.AllowedForNewUsers = true;

    //options.User.RequireUniqueEmail = true;
});

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//});//Added this Policy to authorize all urls and If I want to access any url I can write anonymos annotation


var app = builder.Build();//Building the app


//**********TEST ROLES ********************************
using (var scope = app.Services.CreateScope())
{
    var RoleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //var UserManager = ()scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    bool x = await RoleManager.RoleExistsAsync(Roles.Admin.ToString());
    if (!x)
    {
        var role = new IdentityRole();
        role.Name = Roles.Admin.ToString();
        await RoleManager.CreateAsync(role);
    }
    x = await RoleManager.RoleExistsAsync(Roles.User.ToString());
    if (!x)
    {
        var role = new IdentityRole();
        role.Name = Roles.User.ToString();
        await RoleManager.CreateAsync(role);
    }
}

//**********TEST ROLES ********************************
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

app.UseAuthentication();//Authentication
app.UseAuthorization();//Authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");//Default area is the USER HOME INDEX
app.MapRazorPages();//For Identity Pages


app.Run();//START!!!!!!!!!!!!!!!!!!!!!!!!!!!!










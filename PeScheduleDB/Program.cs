using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using PeScheduleDB.DummyData;
using PeScheduleDB.Areas.Identity.Data;
using Microsoft.AspNetCore.Components.Forms;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PeScheduleDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PeScheduleDBContext") ?? throw new InvalidOperationException("Connection string 'PeScheduleDBContext' not found.")));

builder.Services.AddDefaultIdentity<ScheduleUser>(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<PeScheduleDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

DataForDb.SeedData(app);

using (var scope = app.Services.CreateScope())
{
    var PeDbRoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await PeDbRoleManager.RoleExistsAsync("Teacher"))
        await PeDbRoleManager.CreateAsync(new IdentityRole("Teacher"));
}

using (var scope = app.Services.CreateScope())
{
    var PeDbUserManager = scope.ServiceProvider.GetRequiredService<UserManager<ScheduleUser>>();

    string teacherEmail = "teacher@avcol.school.nz";
    string teacherPassword = "TestTeacher123!";
    

    if (await PeDbUserManager.FindByEmailAsync(teacherEmail) == null)
    {
        var user = new ScheduleUser();
        user.UserName = teacherEmail;
        user.Email = teacherEmail;
        user.FirstName ="Test";
        user.LastName = "Teacher";

        await PeDbUserManager.CreateAsync(user, teacherPassword);
        await PeDbUserManager.AddToRoleAsync(user, "Teacher");
    }
}


app.Run();

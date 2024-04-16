using CorsoNetCore.Models.Entities;
using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.Services.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddTransient<ICoursesService, CoursesService>();

builder.Services.AddDbContextPool<CourcesDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddEntityFrameworkStores<CourcesDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    //app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    app.UseHsts();
} else{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Courses}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapRazorPages();

app.Run();

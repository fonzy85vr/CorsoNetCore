using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.Services.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddTransient<ICoursesService, CoursesService>();

builder.Services.AddDbContextPool<CourcesDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseHsts();
} else{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Courses}/{action=Index}/{id?}");

app.Run();

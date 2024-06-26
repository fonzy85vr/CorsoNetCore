using CorsoNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add services to the container.
Startup.SetupServices(builder);

// Inizialize options
Startup.SetupOptions(builder);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapRazorPages();

app.Run();

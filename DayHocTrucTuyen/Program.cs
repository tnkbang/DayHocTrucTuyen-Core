using DayHocTrucTuyen.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Login";
});

//Using custom authen role
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

//Using SignalR chat
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Using custom status error
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Using Authen cookie
app.UseAuthentication();

app.UseAuthorization();

//Config Endpoint chat
app.MapHub<ChatHub>($"/Models/{nameof(ChatHub)}");

//Config Map Route Areas
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//Default Map Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();

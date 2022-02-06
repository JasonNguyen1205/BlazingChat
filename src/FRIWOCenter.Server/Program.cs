using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FRIWOCenter.Components;
using Microsoft.Extensions.Hosting.Internal;
using FRIWOCenter.Shared.Extensions;
using FRIWOCenter.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

#region ConfigureServices

// setting client host environment 
builder.Services.AddSingleton<IHostEnvironment>(
    new HostingEnvironment() { EnvironmentName = builder.Environment.EnvironmentName });

// adding client app settings 
var applicationSettingsSection = builder.Configuration.GetSection("ApplicationSettings");
builder.Services.Configure<ApplicationSettings>(options =>
{
    applicationSettingsSection.Bind(options);
});

// adding application services
builder.Services.AddFRIWOCenter(applicationSettingsSection.Get<ApplicationSettings>());

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

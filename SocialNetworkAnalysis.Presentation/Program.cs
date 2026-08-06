using SocialNetworkAnalysis.Application.Contracts.Runtime;
using SocialNetworkAnalysis.Infrastructure.Runtime;
using SocialNetworkAnalysis.Presentation.Components;
using SocialNetworkAnalysis.Application;
using SocialNetworkAnalysis.Infrastructure;
using SocialNetworkAnalysis.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var usersJsonPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "Users.Json");
var friendshipsJsonPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "Friendships.Json");
var settingsJsonPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "Settings.Json");

builder.Services.ConfigureCoreServices();
builder.Services.ConfigureInfrastructureServices(usersJsonPath, friendshipsJsonPath, settingsJsonPath);
builder.Services.ConfigureApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

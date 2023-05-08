using Code420.UIOrchestrator.Server.AppStart;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//  Register dependencies (models, classes, orchestrators, services and brokers)
builder.Services
    .AddSyncfusionBlazor()
    .AddMediatR(typeof(Program))
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .RegisterCanxtracServerClasses(builder.Configuration);


var app = builder.Build();

// Add your Syncfusion license key for Blazor platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAxMDA1MUAzMjMwMmUzNDJlMzBXWWtwVlljcWN6cHNSSVBySnl6MVYrY29CeE41bTlTZk83YW03TzE3NFZ3PQ==");

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

using Code420.UIOrchestrator.Server.AppStart;
using FluentValidation;
using MediatR;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

var syncfusionLicense = builder.Configuration.GetSection("SyncfusionLicense").Value;

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//  Register dependencies (models, classes, orchestrators, services and brokers)
builder.Services
    .AddSyncfusionBlazor()
    .AddMediatR(typeof(Program))
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .RegisterUIOrchestratorClasses(builder.Configuration);


var app = builder.Build();

// Add your Syncfusion license key for Blazor platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicense);

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

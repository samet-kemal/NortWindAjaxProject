using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Service;
using Service.Interfaces;
using Service.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddControllersWithViews()
                     .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                     .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    var supportedCulteres = new []
    {
        "en-US",
        "tr-TR"
    };
    options.SetDefaultCulture(supportedCulteres[0])
            .AddSupportedCultures(supportedCulteres)
            .AddSupportedUICultures(supportedCulteres);

});

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ISupplierService,SupplierService>();
builder.Services.AddControllers();

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

var cultures = new[] { "en-US", "tr-TR" };

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(cultures[0])
    .AddSupportedCultures(cultures)
    .AddSupportedUICultures(cultures);

app.UseRequestLocalization(localizationOptions);


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

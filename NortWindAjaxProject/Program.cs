using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using NortWindAjaxProject.Services;
using Service;
using Service.Interfaces;
using Service.Service;
using System.Globalization;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                new CultureInfo("en-US"),
                new CultureInfo("tr-TR")
            };

    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddControllersWithViews()
                     .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                     .AddDataAnnotationsLocalization();


builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ISupplierService,SupplierService>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();




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


app.UseRequestLocalization(options =>
{
    var culture = new List<CultureInfo> { // supported cultures
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = culture;
    options.SupportedUICultures = culture;

    options.RequestCultureProviders.Clear(); // remove predefined RequestCultureProviders
    options.RequestCultureProviders.Add(new RouteValueRequestCultureProvider(culture));
});


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{


    endpoints.MapControllerRoute(
            name: "custom",
        pattern: "{culture=en-US}/{controller=Home}/{action=Index}/{id?}");


});


app.Run();

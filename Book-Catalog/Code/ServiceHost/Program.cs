using BC.Api.Endpoint;
using BC.Application.Contract.Book;
using BC.Application.Implementation;
using BC.Domain.BookAgg;
using BC.Infrastructure.EFCore;
using BC.Infrastructure.EFCore.Repository;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ServiceHost.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture("fa-IR");
    options.AddSupportedUICultures("en-US", "fa-IR");
    options.FallBackToParentUICultures = true;

    options
        .RequestCultureProviders
        .Remove(new AcceptLanguageHeaderRequestCultureProvider());
});

builder.Services
    .AddRazorPages()
    .AddViewLocalization()
    .AddApplicationPart(typeof(BookCatalogController).Assembly);

builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();


var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<BookCatalogDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookApplication, BookApplication>();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRequestLocalizationCookies();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
using CSVFileReader.Data;
using CSVFileReader.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CSVFileDbContext>(options =>
    options.UseSqlite("Data Source=files.db"));
builder.Services.AddTransient<ICSVFileRepository, CsvFileRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Csv}/{action=Index}/{id?}");

app.Run();

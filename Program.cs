using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ContactManager.DAL;
using ContactManager.BL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); //בשביל הסוואגר
builder.Services.AddScoped<ContactBL>();

// SQLite connection (קובץ contacts.db בתיקיית הפרויקט)
var connectionString = builder.Configuration.GetConnectionString("Sqlite") ?? "Data Source=contacts.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// *** הוספת CORS - חשוב! ***
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // Angular dev server
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Ensure DB created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// *** השתמש ב-CORS - לפני UseHttpsRedirection ***
app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
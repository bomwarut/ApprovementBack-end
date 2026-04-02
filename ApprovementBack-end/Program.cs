using ApprovementBack_end.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// เชื่อม DB
builder.Services.AddDbContext<ConnectDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// CORS ให้ Angular เรียกได้
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAngular");
app.MapControllers();
app.Run();
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using employment_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject the database into ASP.NET
var dsn = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<DatabaseContext>(
    x =>
    {
        x.UseSqlServer(
            dsn,
            context =>
            {
                context.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }
        );
    }
    
); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Todo list
/**
 * write CRUD apis to create employees and department as an admin\
note an employee should be under a department 
there should be a relationship btw your employee table and department table
also integrate with verifyMe bvn api and to a validation to check if the firstname,surname, dob all match what is returned from verifyme
 **/
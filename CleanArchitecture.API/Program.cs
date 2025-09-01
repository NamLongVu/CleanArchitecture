using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();

// Add DbContext
builder.Services.AddDbContext<EComDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// (Later) Add authentication, application, and infrastructure services here
builder.Services.AddRateLimiter(o =>
{
    o.AddFixedWindowLimiter(policyName: "fixed", o =>
    {
        o.PermitLimit = 2;
        o.Window = TimeSpan.FromSeconds(5);
        o.QueueLimit = 0;
    });
});
var app = builder.Build();

app.UseRateLimiter();
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
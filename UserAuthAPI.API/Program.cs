using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserAuthAPI.API.DAL;
using UserAuthAPI.API.Domain.Entities;
using UserAuthAPI.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UserAuthAPIContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Conn"));
    });

// Add authentication services
//var jwt = builder.Configuration.GetSection("jwt");

builder.Services.AddAuthentication()
.AddBearerToken(IdentityConstants.BearerScheme); // identity bearer scheme

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<UserAuthAPIContext>()
    .AddApiEndpoints();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 8;
});


builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

await EnsureDatabaseCreatedAsync(app.Services);

app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod());

app.MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger().RequireAuthorization();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task EnsureDatabaseCreatedAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var logger = scopedServices.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = scopedServices.GetRequiredService<UserAuthAPIContext>();
        logger.LogInformation("Applying database migrations...");
        await context.Database.MigrateAsync();


    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
        throw;
    }
}

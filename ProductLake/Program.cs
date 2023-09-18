using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProductLake.Authentication;
using ProductLake.DataManagerService;
using ProductLake.HealthCheck;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(item =>
{
    item.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearer =>
{
    bearer.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHealthChecks()
.AddCheck<ProductDataStoreHealthCheck>(ProductDataStoreHealthCheck.Name);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register user defined service.
builder.Services.AddSingleton<IDataStoreServiceFactory, DataStoreServiceFactory>();
//builder.Services.AddSingleton<IAuthenticationService, AccessCodeAuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseMiddleware<AuthenticationMiddleware>();
app.MapHealthChecks("/_internalHealthCheck").AllowAnonymous();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

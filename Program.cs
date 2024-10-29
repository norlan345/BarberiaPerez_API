
using BarberiaPerez_API.Services;
using BarberiaPerez_API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cargar la clave JWT desde la configuraci�n de MongoDbSettings
var key = builder.Configuration.GetSection("MongoDbSettings").GetSection("Token").Value;

// Configuraci�n de autenticaci�n JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost",
            ValidAudience = "https://localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Cargar la configuraci�n desde el archivo appsettings.json para MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

// A�adir `MongoDbSettings` como un singleton para que est� disponible en los servicios
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value
);

// Registrar IUsuarioService e inyectar UsuarioService
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// A�adir los controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null); // Para evitar cambiar el formato de nombres en JSON

// Configurar Swagger para documentar la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();

//using BarberiaPerez_API.Services;
//using BarberiaPerez_API.Settings;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.Extensions.Options;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Configuraci�n de JWT
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtSettings["Issuer"],
//            ValidAudience = jwtSettings["Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
//        };
//    });

//// Configuraci�n de la base de datos MongoDB
//builder.Services.Configure<MongoDbSettings>(
//    builder.Configuration.GetSection("MongoDbSettings"));

//// Registrar `MongoDbSettings` como un singleton para inyecci�n de dependencia
//builder.Services.AddSingleton(sp =>
//    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

//// Registrar servicios de la aplicaci�n
//builder.Services.AddSingleton<UsuarioService>();

//// Configuraci�n de controladores y opciones JSON
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//        options.JsonSerializerOptions.PropertyNamingPolicy = null); // Para evitar el cambio de formato en los nombres JSON

//// Configuraci�n de Swagger para documentaci�n de API
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configuraci�n del pipeline HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// Habilitar autenticaci�n y autorizaci�n
//app.UseAuthentication();
//app.UseAuthorization();

//// Mapear los controladores
//app.MapControllers();

//app.Run();


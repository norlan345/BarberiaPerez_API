
using BarberiaPerez_API.Services;
using BarberiaPerez_API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cargar la clave JWT desde la configuración de MongoDbSettings
var key = builder.Configuration.GetSection("MongoDbSettings").GetSection("Token").Value;

// Configuración de autenticación JWT
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

// Cargar la configuración desde el archivo appsettings.json para MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

// Añadir `MongoDbSettings` como un singleton para que esté disponible en los servicios
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value
);

// Registrar IUsuarioService e inyectar UsuarioService
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Añadir los controladores
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

// Habilitar autenticación y autorización
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

//// Configuración de JWT
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

//// Configuración de la base de datos MongoDB
//builder.Services.Configure<MongoDbSettings>(
//    builder.Configuration.GetSection("MongoDbSettings"));

//// Registrar `MongoDbSettings` como un singleton para inyección de dependencia
//builder.Services.AddSingleton(sp =>
//    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

//// Registrar servicios de la aplicación
//builder.Services.AddSingleton<UsuarioService>();

//// Configuración de controladores y opciones JSON
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//        options.JsonSerializerOptions.PropertyNamingPolicy = null); // Para evitar el cambio de formato en los nombres JSON

//// Configuración de Swagger para documentación de API
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configuración del pipeline HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// Habilitar autenticación y autorización
//app.UseAuthentication();
//app.UseAuthorization();

//// Mapear los controladores
//app.MapControllers();

//app.Run();


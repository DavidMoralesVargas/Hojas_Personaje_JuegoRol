using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Usuarios;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Vampiros;
using HojasPersonaje.Backend.Repositorios.Interfaces.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Usuarios;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using HojasPersonaje.Backend.Services.Implementaciones.Usuarios;
using HojasPersonaje.Backend.Services.Implementaciones.Vampiros;
using HojasPersonaje.Backend.Services.Interfaces.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Vampiros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);



//Configurar CORS para que solo el Frontend en Vue pueda acceder a él
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // 👈 dominio permitido
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//Conexión a base de datos PostgreSQL
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSqlConnection")));

//Repositorios
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IDisciplinasRepository, DisciplinasRepository>();
builder.Services.AddScoped<IVampiroRepository, VampiroRepository>();


//Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDisciplinasServices, DisciplinasServices>();
builder.Services.AddScoped<IVampirosServices, VampirosServices>();


//Configuramos autenticación de Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
    ClockSkew = TimeSpan.Zero
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend"); //Activamos los CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

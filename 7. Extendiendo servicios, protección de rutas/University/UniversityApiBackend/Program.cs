using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;

var builder = WebApplication.CreateBuilder(args);

// 1. String de conexion.

const string CONNECTIONNAME = "UniversityDb";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 2. Agregamos el contexto.

builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connectionString));



// Add services to the container.



builder.Services.AddJwtTokenServices(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddScoped<IStudentsService, StudentsService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User 1"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Autorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Autenticación JWT con esquema Bearer en el encabezado"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });;
});



builder.Services.AddCors(options => {
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

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

app.UseCors("CorsPolicy");

app.Run();

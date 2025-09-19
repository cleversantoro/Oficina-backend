using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Endpoints;
using Oficina.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Conn string via env var "ConnectionStrings__Default"
var connectionString = builder.Configuration.GetConnectionString("Default")  ?? builder.Configuration["ConnectionStrings:Default"];

builder.Services.AddDbContext<OficinaDbContext>(opt => opt.UseNpgsql(connectionString));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new() { Title = "Oficina API", Version = "v1" });
});

// Healthcheck simples
builder.Services.AddHealthChecks();
    //.AddNpgSql(connectionString);

// CORS para o Angular
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("default", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true));
});


// Cadastro (DI do módulo)
builder.Services.AddCadastroModule(builder.Configuration);

var app = builder.Build();

app.UseCors("default");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new { name = "Oficina API", status = "ok" }));
app.MapGet("/health", () => Results.Ok("healthy"));
app.MapHealthChecks("/healthz");

app.MapCadastroEndpoints();

app.Run();


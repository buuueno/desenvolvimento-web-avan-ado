using Microsoft.EntityFrameworkCore;
using GestaoTarefasApi.Data;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ✅ CORS obrigatório
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Swagger obrigatório
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "API - Gestão de Tarefas",
        Version = "v1",
        Description = "API REST para gerenciamento de tarefas, categorias e responsáveis."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestão de Tarefas v1"));
}

app.UseHttpsRedirection();
app.UseCors("PermitirTudo");
app.UseAuthorization();
app.MapControllers();

app.Run();


using Microsoft.EntityFrameworkCore;
using PrioridadesApi.Data;
using PrioridadesApi.Models;

var builder = WebApplication.CreateBuilder(args);

const string CorsPolicy = "AllowAll";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(CorsPolicy, p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var cn = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? "Data Source=prioridades.db";
    opt.UseSqlite(cn);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(CorsPolicy);

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

var group = app.MapGroup("/api/prioridades").WithTags("Prioridades");

group.MapGet("/", async (AppDbContext db) =>
    Results.Ok(await db.Prioridades.AsNoTracking()
        .OrderBy(p => p.Nivel).ThenBy(p => p.Id).ToListAsync())
);

group.MapGet("/{id:int}", async (int id, AppDbContext db) =>
{
    var p = await db.Prioridades.FindAsync(id);
    return p is null ? Results.NotFound() : Results.Ok(p);
});

group.MapPost("/", async (Prioridad body, AppDbContext db) =>
{
    body.Id = 0;
    body.CreadaEl = DateTime.UtcNow;
    db.Prioridades.Add(body);
    await db.SaveChangesAsync();
    return Results.Created($"/api/prioridades/{body.Id}", body);
});

group.MapPut("/{id:int}", async (int id, Prioridad body, AppDbContext db) =>
{
    var p = await db.Prioridades.FindAsync(id);
    if (p is null) return Results.NotFound();

    p.Titulo = body.Titulo;
    p.Descripcion = body.Descripcion;
    p.Nivel = body.Nivel;
    p.FechaVencimiento = body.FechaVencimiento;
    p.Completada = body.Completada;
    p.ActualizadaEl = DateTime.UtcNow;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

group.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
{
    var p = await db.Prioridades.FindAsync(id);
    if (p is null) return Results.NotFound();

    db.Prioridades.Remove(p);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

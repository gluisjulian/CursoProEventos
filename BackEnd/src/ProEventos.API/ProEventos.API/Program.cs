using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProEventos.Application.Interface;
using ProEventos.Application.Interface.Implementations;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interface;
using ProEventos.Persistence.Interface.Implementations;

var builder = WebApplication.CreateBuilder(args);


//Database
builder.Services.AddDbContext<ProEventosContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("ProEventos")));


//Dependecy Injection
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IEventoPersistence, EventoPersistence>();
builder.Services.AddScoped<IGeralPersistence, GeralPersistence>();



builder.Services.AddControllers()
    .AddNewtonsoftJson(
        x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin()
);


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});


app.MapControllers();

app.Run();

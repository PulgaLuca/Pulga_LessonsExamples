/*
 * Before starting, install the following NuGet packages:
 * dotnet add package mediatR --version 12.0.1
 */

using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Json.Devices.Lightning;

// Creazione del builder dell'applicazione ASP.NET Core (analogo a App.xaml in WPF)
// builder serve per configurare servizi, e impostazioni dell'app
var builder = WebApplication.CreateBuilder(args);


// REGISTRAZIONE SERVIZI
// abilita il supporto ai Controller API, permette di creare endpoint API tramite classi Controller
builder.Services.AddControllers();

// aggiunge il supporto per l'esplorazione degli endpoint API, necessario per Swagger
builder.Services.AddEndpointsApiExplorer();

// configurazione std Swagger per generare la documentazione delle API
builder.Services.AddSwaggerGen();


// CONFIGURAZIONE MEDIATR
// Registrazione di MediatR nel Dependency Injection Container (we already know that)
//
// MediatR implementa il pattern "Mediator", dunque:
// i Controller non chiamano direttamente i servizi,
// ma inviano "Command" o "Query" a un Handler, chiamato indirettamente da MediatR (dunque non gestiamo direttamente).
//
// typeof(AddLampCommandHandler).Assembly
// indica a MediatR dove cercare automaticamente gli Handler
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AddLampCommandHandler).Assembly));

// DEPENDENCY INJECTION
// Registrazione del repository come Singleton (we already know that)
//
// Singleton, è uno dei Design Pattern che vedremo, significa:
// viene creata UNA SOLA istanza per tutta la vita dell'applicazione
//
// Quando una classe richiede ILampRepository,
// ASP.NET Core fornirà automaticamente JsonLampRepository
builder.Services.AddSingleton<ILampRepository, JsonLampRepository>();

var app = builder.Build();

// Controlla se l'app è in modalità Development
// (sviluppo locale)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Reindirizza automaticamente HTTP -> HTTPS
// Migliora la sicurezza della comunicazione
app.UseHttpsRedirection();

// Collega automaticamente i Controller agli endpoint HTTP
// Esempio:
// [HttpGet] -> GET
// [HttpPost] -> POST
app.MapControllers();

// Avvia il web server e mette in esecuzione l'applicazione
app.Run();
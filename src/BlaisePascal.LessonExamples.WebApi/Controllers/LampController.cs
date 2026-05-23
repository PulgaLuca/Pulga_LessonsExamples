using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAll;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetLampById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlaisePascal.LessonsExamples.API.Controllers;

// Indica che questa classe è un Controller API REST
// ASP.NET Core abilita automaticamente varie cose come:
// - validazione dei dati input
// - binding automatico del JSON
// - gestione degli errori HTTP
[ApiController]

// Definisce la route base del controller
// Tutti gli endpoint inizieranno con: api/lamps
[Route("api/lamps")]

public sealed class LampController : ControllerBase
{
    private readonly IMediator _mediator;

    public LampController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // GET api/lamps
    // Questo metodo risponde alle richieste HTTP GET verso api/lamps
    [HttpGet]
    public async Task<ActionResult<List<LampDto>>> GetAll(CancellationToken cancellationToken)
    {
        // cancellationToken permette di interrompere l'operazione se il client annulla la richiesta
        var result = await _mediator.Send(new GetAllLampsQuery(), cancellationToken);

        // Se l'operazione è andata bene -> HTTP 200 OK con i dati
        // Se qualcosa è andato storto: -> HTTP 404 Not Found con messaggio errore
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }


    // GET api/lamps/{id}
    // Endpoint GET con parametro nella route
    // {id:guid} significa: il parametro deve essere un GUID valido
    //
    // Esempio:
    // api/lamps/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpGet("{id:guid}")]

    public async Task<ActionResult<LampDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLampByIdQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }


    // POST api/lamps
    // Questo endpoint gestisce richieste HTTP POST usate per creare nuove risorse
    [HttpPost]

    // [FromBody] indica che il comando viene letto dal body JSON della richiesta HTTP
    // Esempio JSON:
    // {
    //    "name": "Lampada cucina",
    //    "power": 60
    // }
    public async Task<ActionResult<Guid>> Add([FromBody] AddLampCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        // Se la creazione è avvenuta correttamente:
        // CreatedAtAction restituisce:
        // - HTTP 201 Created
        // - URL della nuova risorsa creata
        // - id creato
        //
        // nameof(GetById) evita di scrivere il nome del metodo come stringa manuale
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value) : BadRequest(result.Error);
    }
}

//[ApiController]
//[Route("api/lamps")]
//public sealed class LampController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public LampController(IMediator mediator) => _mediator = mediator;

//    // GET api/lamps
//    [HttpGet]
//    public async Task<Results<Ok<List<LampDto>>, NotFound<Error>, BadRequest<Error>>> GetAll(
//        CancellationToken cancellationToken)
//    {
//        var result = await _mediator.Send(new GetAllLampsQuery(), cancellationToken);

//        return result.IsSuccess
//            ? TypedResults.Ok(result.Value)
//            : TypedResults.NotFound(result.Error);
//    }

//    // GET api/lamps/{id}
//    [HttpGet("{id:guid}")]
//    public async Task<Results<Ok<LampDto>, NotFound<Error>>> GetById(
//        Guid id,
//        CancellationToken cancellationToken)
//    {
//        var result = await _mediator.Send(new GetLampByIdQuery(id), cancellationToken);

//        return result.IsSuccess
//            ? TypedResults.Ok(result.Value)
//            : TypedResults.NotFound(result.Error);
//    }

//    // POST api/lamps
//    [HttpPost]
//    public async Task<Results<CreatedAtRoute<Guid>, BadRequest<Error>>> Add(
//        [FromBody] AddLampCommand command,
//        CancellationToken cancellationToken)
//    {
//        var result = await _mediator.Send(command, cancellationToken);

//        return result.IsSuccess
//            ? TypedResults.CreatedAtRoute(result.Value, nameof(GetById), new { id = result.Value })
//            : TypedResults.BadRequest(result.Error);
//    }

// DELETE api/lamps/{id}
//[HttpDelete("{id:guid}")]
//public async Task<Results<NoContent, NotFound<Error>>> Remove(
//    Guid id,
//    CancellationToken cancellationToken)
//{
//    var result = await _mediator.Send(new RemoveLampCommand(id), cancellationToken);

//    return result.IsSuccess
//        ? TypedResults.NoContent()
//        : TypedResults.NotFound(result.Error);
//}

// PATCH api/lamps/{id}/switch-on
//[HttpPatch("{id:guid}/switch-on")]
//public async Task<Results<NoContent, NotFound<Error>, Conflict<Error>>> SwitchOn(
//    Guid id,
//    CancellationToken cancellationToken)
//{
//    var result = await _mediator.Send(new SwitchOnLampCommand(id), cancellationToken);

//    if (result.IsSuccess)
//        return TypedResults.NoContent();

//    return result.Error.Type switch
//    {
//        ErrorType.NotFound => TypedResults.NotFound(result.Error),
//        ErrorType.Conflict => TypedResults.Conflict(result.Error),
//        _ => TypedResults.Conflict(result.Error)
//    };
//}

// PATCH api/lamps/{id}/switch-off
//[HttpPatch("{id:guid}/switch-off")]
//public async Task<Results<NoContent, NotFound<Error>, Conflict<Error>>> SwitchOff(
//    Guid id,
//    CancellationToken cancellationToken)
//{
//    var result = await _mediator.Send(new SwitchOffLampCommand(id), cancellationToken);

//    if (result.IsSuccess)
//        return TypedResults.NoContent();

//    return result.Error.Type switch
//    {
//        ErrorType.NotFound => TypedResults.NotFound(result.Error),
//        ErrorType.Conflict => TypedResults.Conflict(result.Error),
//        _ => TypedResults.Conflict(result.Error)
//    };
//}

// PATCH api/lamps/{id}/brightness
//[HttpPatch("{id:guid}/brightness")]
//public async Task<Results<NoContent, NotFound<Error>, BadRequest<Error>>> ChangeBrightness(
//    Guid id,
//    [FromBody] ChangeBrightnessRequest body,
//    CancellationToken cancellationToken)
//{
//    var result = await _mediator.Send(new ChangeBrightnessCommand(id, body.Intensity), cancellationToken);

//    if (result.IsSuccess)
//        return TypedResults.NoContent();

//    return result.Error.Type switch
//    {
//        ErrorType.NotFound => TypedResults.NotFound(result.Error),
//        _ => TypedResults.BadRequest(result.Error)
//    };
//}

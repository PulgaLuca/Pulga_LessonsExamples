using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAll;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetLampById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlaisePascal.LessonsExamples.API.Controllers;

[ApiController]
[Route("api/lamps")]
public sealed class LampController : ControllerBase
{
    private readonly IMediator _mediator;

    public LampController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET api/lamps
    [HttpGet]
    public async Task<ActionResult<List<LampDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllLampsQuery(), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    // GET api/lamps/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LampDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLampByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    // POST api/lamps
    [HttpPost]
    public async Task<ActionResult<Guid>> Add([FromBody] AddLampCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value)
            : BadRequest(result.Error);
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

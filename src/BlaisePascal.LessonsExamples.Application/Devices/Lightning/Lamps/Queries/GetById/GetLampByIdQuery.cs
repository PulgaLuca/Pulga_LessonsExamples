using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetLampById;

public sealed record GetLampByIdQuery(Guid Id) : IRequest<Result<LampDto>>;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetById
{
    public sealed record GetLampByIdQuery(Guid Id) : IRequest<Result<LampDto>>;
}

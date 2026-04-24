using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAll
{
    public sealed record GetAllLampsQuery() : IRequest<Result<List<LampDto>>>;
}

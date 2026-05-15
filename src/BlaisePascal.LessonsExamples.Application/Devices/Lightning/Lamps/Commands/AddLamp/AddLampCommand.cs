using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp
{
    public sealed record AddLampCommand(string Name, string ImageUrl) : IRequest<Result<Guid>>;
}

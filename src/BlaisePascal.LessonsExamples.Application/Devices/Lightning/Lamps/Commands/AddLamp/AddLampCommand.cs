using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp
{
    //public class AddLampCommand
    //{
    //    private readonly ILampRepository _repository;

    //    public AddLampCommand(ILampRepository repository)
    //    {
    //        _repository = repository;
    //    }

    //    public void Execute(string name, string imageUrl)
    //    {
    //        var lamp = new Lamp(new DeviceName(name), new DeviceImage(imageUrl));
    //        _repository.Add(lamp);
    //    }
    //}

    public sealed record AddLampCommand(string Name, string ImageUrl) : IRequest<Result<Guid>>;
}

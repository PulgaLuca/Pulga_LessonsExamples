using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp
{
    public sealed class AddLampCommandHandler : IRequestHandler<AddLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<AddLampCommandHandler> _logger;

        public AddLampCommandHandler(ILampRepository repository, IMediator mediator, ILogger<AddLampCommandHandler> logger)
        {
            _repository = repository;
            _mediator = mediator;
            _logger = logger;
        }

        public Task<Result<Guid>> Handle(AddLampCommand request, CancellationToken cancellationToken)
        {
            var nameResult = new DeviceName(request.Name);
            var imageResult = new DeviceImage (request.ImageUrl);
            var lamp = new Lamp(nameResult, imageResult);

            var result = _repository.Add(lamp);

            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            return Task.FromResult(Result.Success(lamp.Id));
        }
    }
}

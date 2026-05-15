using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp
{
    public sealed class AddLampCommandHandler : IRequestHandler<AddLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _repository;   

        public AddLampCommandHandler(ILampRepository repository)
        {
            _repository = repository;
        }

        // MediatR richiede che il metodo Handle restituisca un Task<T>.
        // Questo permette di supportare operazioni asincrone
        // (database, API, file system, ecc.) senza bloccare il thread.

        // Mentre, il CancellationToken permette di interrompere l'operazione,
        // ovvero, se la richiesta HTTP viene annullata o scade il timeout.
        // È molto utile nelle operazioni lunghe o asincrone.
        public Task<Result<Guid>> Handle(AddLampCommand request, CancellationToken cancellationToken)
        {
            var nameResult = new DeviceName(request.Name);
            var imageResult = new DeviceImage(request.ImageUrl);

            var lamp = new Lamp(nameResult, imageResult);

            var result = _repository.Add(lamp);

            // se il repository fallisce per qualche motivo restituiamo subito un Task completato
            // contenente il Result di errore, come già visto.
            // Task.FromResult viene usato perché qui non abbiamo codice async reale,
            // ma la firma del metodo richiede comunque Task<Result<Guid>>.
            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            return Task.FromResult(Result.Success(lamp.Id));
        }
    }
}

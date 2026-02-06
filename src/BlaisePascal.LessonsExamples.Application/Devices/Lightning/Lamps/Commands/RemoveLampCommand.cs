using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands
{
    public class RemoveLampCommand
    {
        private readonly ILampRepository _repository;

        public RemoveLampCommand(ILampRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Guid lampId)
        {
            _repository.Remove(lampId);
        }
    }
}

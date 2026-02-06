using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands
{
    public class SwitchOffLampCommand
    {
        private readonly ILampRepository _repository;

        public SwitchOffLampCommand(ILampRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Guid lampId)
        {
            var lamp = _repository.GetById(lampId);
            if (lamp != null)
            {
                lamp.SwitchOff();
                _repository.Update(lamp);
            }
        }
    }
}

using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands
{
    public class SwitchOffLampCommand
    {
        private ILampRepository _repository;

        public SwitchOffLampCommand(ILampRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Guid lampId) // L'utente mi comunica quale lampada spegnere
        {
            var lamp = _repository.GetById(lampId); // Leggo la lampada da file con quell'id passato da utente
            if (lamp != null)
            {
                 lamp.SwitchOff(); // cambio lo stato a livello Domain
                _repository.Update(lamp); // Salvo il nuovo stato della lampada modificata sul file
            }
        }
    }
}

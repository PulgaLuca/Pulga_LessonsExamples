using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands
{
    public class ChangeIntensityCommand
    {
        private readonly ILampRepository _repository;

        public ChangeIntensityCommand(ILampRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Guid lampId, int intensity)
        {
            var lamp = _repository.GetById(lampId);
            if (lamp != null)
            {
                lamp.ChangeBrightnessTo(intensity);
                _repository.Update(lamp);
            }
        }
    }
}

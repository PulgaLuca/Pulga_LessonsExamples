using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using System.Xml;

namespace BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.InMemory
{
    public class InMemoryLampRepository : ILampRepository
    {
        private readonly List<Lamp> _lamps;

        public InMemoryLampRepository()
        {
            // HARD-CODED
            _lamps = new List<Lamp>
            {
                new Lamp(new DeviceName("Crazy Lamp"), new DeviceImage("")),
                new Lamp(new DeviceName("Pascal Lamp"), new DeviceImage("")),
                new Lamp(new DeviceName("Pulgs Lamp"), new DeviceImage(""))
            };
        }

        public List<Lamp> GetAll()
        {
            return _lamps;
        }

        public Lamp GetById(Guid id)
        {
            return _lamps.First(lamp => lamp.Id == id);
        }

        public void Add(Lamp lamp)
        {
            if (lamp == null)
                throw new ArgumentNullException(nameof(lamp));

            _lamps.Add(lamp);
        }

        public void Remove(Guid id)
        {
            var lamp = GetById(id);

            if (lamp != null)
                _lamps.Remove(lamp);
        }

        public void Update(Lamp lamp)
        {
            // Actually not to do
        }
    }
}

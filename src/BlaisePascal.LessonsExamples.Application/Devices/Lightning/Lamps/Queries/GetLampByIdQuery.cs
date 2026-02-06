using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries
{
    public class GetLampByIdQuery
    {
        private readonly ILampRepository _repository;

        public GetLampByIdQuery(ILampRepository repository)
        {
            _repository = repository;
        }

        public LampDto Execute(Guid id)
        {
            var l = _repository.GetById(id);
            return new LampDto
            {
                Id = l.Id,
                Name = l.Name.Value,
                Status = l.Status == DeviceStatus.On ? "ON" : "OFF",
                Brightness = l.Brightness.Value
            };
        }
    }
}

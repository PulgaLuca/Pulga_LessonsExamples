using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries
{
    public class GetAllLampsQuery
    {
        private readonly ILampRepository _repository;

        public GetAllLampsQuery(ILampRepository repository)
        {
            _repository = repository;
        }

        //public List<LampDto> Execute()
        //{
        //    return _repository.GetAll()
        //        .Select(l => new LampDto
        //        {
        //            Id = l.Id,
        //            Name = l.Name.Value,
        //            Status = l.Status == DeviceStatus.On ? "ON" : "OFF",
        //            Brightness = l.Brightness.Value
        //        }).ToList();
        //}

        public List<LampDto> Execute()
        {
            var result = new List<LampDto>();

            foreach (var l in _repository.GetAll())
            {
                result.Add(new LampDto
                {
                    Id = l.Id,
                    Name = l.Name.Value,
                    ImageUrl = l.ImageUrl.imageUrl,
                    Status = l.Status == DeviceStatus.On ? "ON" : "OFF",
                    Brightness = l.Brightness.Value,
                    CreatedAtUtc = l.CreatedAtUtc,
                    LastModifiedAtUtc = l.LastModifiedAtUtc
                });
            }

            return result;
        }
    }
}

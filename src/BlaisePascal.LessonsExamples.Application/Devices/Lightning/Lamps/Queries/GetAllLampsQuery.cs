using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
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


        public List<LampDto> Execute()
        {
            var result = new List<LampDto>();

            foreach (var l in _repository.GetAll())
            {
                result.Add(LampMapper.ToDto(l));
            }

            return result;
        }
    }
}

using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAll
{
    public sealed class GetAllLampsQueryHandler : IRequestHandler<GetAllLampsQuery, Result<List<LampDto>>>
    {
        private readonly ILampRepository _repository;
        private readonly ILogger<GetAllLampsQueryHandler> _logger;

        public GetAllLampsQueryHandler(ILampRepository repository, ILogger<GetAllLampsQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<Result<List<LampDto>>> Handle(GetAllLampsQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.GetAll();

            if (result.IsFailure)
            {
                _logger.LogError("Failed to get lamps: {Error}", result.Error);
                return Task.FromResult(Result.Failure<List<LampDto>>(result.Error));
            }

            var dtos = Map(result.Value);

            return Task.FromResult(Result.Success(dtos));
        }

        private static List<LampDto> Map(List<Lamp> lamps)
        {
            return lamps
                .Select(LampMapper.ToDto)
                .ToList();
        }
    }
}
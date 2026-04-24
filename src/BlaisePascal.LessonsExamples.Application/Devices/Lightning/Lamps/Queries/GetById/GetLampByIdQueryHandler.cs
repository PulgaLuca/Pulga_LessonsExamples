using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetById
{
    public sealed class GetLampByIdQueryHandler : IRequestHandler<GetLampByIdQuery, Result<LampDto>>
    {
        private readonly ILampRepository _repository;
        private readonly ILogger<GetLampByIdQueryHandler> _logger;

        public GetLampByIdQueryHandler(ILampRepository repository, ILogger<GetLampByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<Result<LampDto>> Handle(GetLampByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.GetById(request.Id);

            if (result.IsFailure)
            {
                _logger.LogWarning("Lamp not found: {LampId}", request.Id);
                return Task.FromResult(Result.Failure<LampDto>(result.Error));
            }

            var dto = LampMapper.ToDto(result.Value);

            return Task.FromResult(Result.Success(dto));
        }
    }
}
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAll;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAllLamps;

public sealed class GetAllLampsQueryHandler : IRequestHandler<GetAllLampsQuery, Result<List<LampDto>>>
{
    private readonly ILampRepository _repository;

    public GetAllLampsQueryHandler(ILampRepository repository) => _repository = repository;

    public Task<Result<List<LampDto>>> Handle(GetAllLampsQuery request, CancellationToken cancellationToken)
    {
        var result = _repository.GetAll();

        if (result.IsFailure)
            return Task.FromResult(Result.Failure<List<LampDto>>(result.Error));

        var dtos = result.Value.Select(LampMapper.ToDto).ToList();
        return Task.FromResult(Result.Success(dtos));
    }
}
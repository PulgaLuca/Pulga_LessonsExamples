// Application/Devices/Lightning/Lamps/Queries/GetLampById/GetLampByIdQueryHandler.cs
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using MediatR;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetLampById;

public sealed class GetLampByIdQueryHandler : IRequestHandler<GetLampByIdQuery, Result<LampDto>>
{
    private readonly ILampRepository _repository;

    public GetLampByIdQueryHandler(ILampRepository repository) => _repository = repository;

    public Task<Result<LampDto>> Handle(GetLampByIdQuery request, CancellationToken cancellationToken)
    {
        var result = _repository.GetById(request.Id);

        if (result.IsFailure)
            return Task.FromResult(Result.Failure<LampDto>(result.Error));

        return Task.FromResult(Result.Success(LampMapper.ToDto(result.Value)));
    }
}
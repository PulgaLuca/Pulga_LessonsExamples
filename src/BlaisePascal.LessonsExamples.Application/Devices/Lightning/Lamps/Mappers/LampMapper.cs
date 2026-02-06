using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Mappers;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers
{
    public class LampMapper
    {
        public static LampDto ToDto(Lamp lamp)
        {
            return new LampDto
            {
                Id = lamp.Id,
                Name = lamp.Name.Value,
                ImageUrl = lamp.ImageUrl.imageUrl,
                Status = DeviceStatusMapper.ToDto(lamp.Status),
                Brightness = lamp.Brightness.Value,
                CreatedAtUtc = lamp.CreatedAtUtc,
                LastModifiedAtUtc = lamp.LastModifiedAtUtc
            };
        }

        public static Lamp ToDomain(LampDto dto)
        {
            return new Lamp(
                dto.Id,
                new DeviceName(dto.Name),
                new DeviceImage(dto.ImageUrl),
                DeviceStatusMapper.ToDomain(dto.Status),
                Brightness.From(dto.Brightness),
                dto.CreatedAtUtc,
                dto.LastModifiedAtUtc);
        }
    }
}

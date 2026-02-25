using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;

namespace BlaisePascal.LessonsExamples.Application.Devices.Abstraction.Mappers
{
    public static class DeviceStatusMapper
    {
        public static string ToDto(DeviceStatus status)
        {
            return status switch
            {
                DeviceStatus.On => "ON",
                DeviceStatus.Off => "OFF",
                DeviceStatus.Standby => "STANDBY",
                DeviceStatus.Error => "ERROR",
                _ => "UNKNOWN"
            };
        }

        public static DeviceStatus ToDomain(string status)
        {
            return status switch
            {
                 "ON" => DeviceStatus.On,
                 "OFF" => DeviceStatus.Off,
                 "STANDBY" => DeviceStatus.Standby,
                 "ERROR" => DeviceStatus.Error,
                _ => DeviceStatus.Unknown
            };
        }
    }

}

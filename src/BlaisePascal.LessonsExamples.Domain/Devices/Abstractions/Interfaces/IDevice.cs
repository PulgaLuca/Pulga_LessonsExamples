using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces
{
    public interface IDevice : IAuditable, ISwitchable
    {
        Guid Id { get; }

        DeviceName Name { get; }

        DeviceStatus Status { get; }

        DeviceImage? ImageUrl { get; }

        Result Rename(DeviceName name);

        Result ChangeImage(DeviceImage image);
    }
}
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces
{
    public interface IDevice : IAuditable, ISwitchable
    {
        Guid Id { get; }
        DeviceName Name { get; }
        DeviceStatus Status { get; }
        DeviceImage? ImageUrl { get; }

        void Rename(DeviceName name);
        void ChangeImage(DeviceImage image);
    }
}

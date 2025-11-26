using BlaisePascal.LessonsExamples.Domain.Shared;

namespace BlaisePascal.LessonsExamples.Domain.Abstractions
{
    public interface IDevice
    {
        Guid Id { get; }
        string Name { get; }
        string? ImageUrl { get; }

        DeviceStatus Status { get; }

        DateTime CreatedAtUtc { get; }
        DateTime LastModifiedAtUtc { get; }

        void SwitchOn();
        void SwitchOff();
    }
}

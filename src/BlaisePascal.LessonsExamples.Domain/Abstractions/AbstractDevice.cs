using BlaisePascal.LessonsExamples.Domain.Abstractions;

namespace BlaisePascal.LessonsExamples.Domain.Shared
{
    public abstract class AbstractDevice : IDevice
    {
        public Guid Id { get; }
        public string Name { get; protected set; }
        public string? ImageUrl { get; protected set; }

        public DeviceStatus Status { get; protected set; }

        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime LastModifiedAtUtc { get; protected set; }

        protected AbstractDevice(string name, string? imageUrl = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ImageUrl = imageUrl;

            Status = DeviceStatus.Off;
            CreatedAtUtc = DateTime.UtcNow;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void SwitchOn()
        {
            if (Status == DeviceStatus.On)
                throw new InvalidOperationException($"{Name} is already on.");

            Status = DeviceStatus.On;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException($"{Name} is already off.");

            Status = DeviceStatus.Off;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}

using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions
{
    public abstract class AbstractDevice : IDevice
    {
        public Guid Id { get; }
        public DeviceName Name { get; protected set; }
        public DeviceImage? ImageUrl { get; protected set; }

        public DeviceStatus Status { get; protected set; }

        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime LastModifiedAtUtc { get; protected set; }

        protected AbstractDevice(DeviceName name, DeviceImage? imageUrl = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ImageUrl = imageUrl;

            Status = DeviceStatus.Off;
            CreatedAtUtc = DateTime.UtcNow;
            Touch();
        }

        public virtual void SwitchOn()
        {
            EnsureDeviceIsOn();
            Status = DeviceStatus.On;
            Touch();
        }

        public virtual void SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException($"{Name} is already off.");

            Status = DeviceStatus.Off;
            Touch();
        }

        public void Rename(DeviceName name)
        {
            Name = name;
            Touch();
        }

        public void ChangeImage(DeviceImage image)
        {
            ImageUrl = image;
            Touch();
        }

        public void EnsureDeviceIsOn()
        {
            if (Status != DeviceStatus.On)
                throw new DomainException("Device must be on.");
        }

        public void EnsureDeviceIsOff()
        {
            if (Status != DeviceStatus.Off)
                throw new DomainException("Device is already off.");
        }

        public void Touch()
        {
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}

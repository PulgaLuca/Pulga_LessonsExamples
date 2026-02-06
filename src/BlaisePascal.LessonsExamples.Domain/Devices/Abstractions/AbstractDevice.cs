using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions
{
    public abstract class AbstractDevice : IDevice
    {
        public Guid Id { get; set; }
        public DeviceName Name { get; set; }
        public DeviceImage ImageUrl { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastModifiedAtUtc { get; set; }

        protected AbstractDevice(DeviceName name, DeviceImage imageUrl)
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

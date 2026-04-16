using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions
{
    public abstract class AbstractDevice : Entity, IDevice
    {
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
            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();
        }

        public virtual void SwitchOff()
        {
            EnsureDeviceIsOff();
            Status = DeviceStatus.Off;
            Raise(new DeviceSwitchedOffEvent(Id));
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

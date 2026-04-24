using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.SharedKernel;
using BlaisePascal.SmartHouse.Domain.Devices.LuminousDevices.Errors;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions
{
    public abstract class AbstractDevice : Entity, IDevice
    {
        public DeviceName Name { get; protected set; }
        public DeviceImage ImageUrl { get; protected set; }
        public DeviceStatus Status { get; protected set; }
        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime LastModifiedAtUtc { get; protected set; }

        protected AbstractDevice(DeviceName name, DeviceImage imageUrl)
        {
            Id = Guid.NewGuid();
            Name = name;
            ImageUrl = imageUrl;

            Status = DeviceStatus.Off;
            CreatedAtUtc = DateTime.UtcNow;
            Touch();
        }

        public virtual Result SwitchOn()
        {
            if (Status == DeviceStatus.On)
                return Result.Failure(LampErrors.AlreadyOn);

            Status = DeviceStatus.On;

            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();

            return Result.Success();
        }

        public virtual Result SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                return Result.Failure(LampErrors.AlreadyOff);

            Status = DeviceStatus.Off;

            Raise(new DeviceSwitchedOffEvent(Id));
            Touch();

            return Result.Success();
        }

        public Result Rename(DeviceName name)
        {
            if (name == Name)
                return Result.Success();

            Name = name;
            Touch();

            return Result.Success();
        }

        public Result ChangeImage(DeviceImage image)
        {
            if (image == ImageUrl)
                return Result.Success();

            ImageUrl = image;
            Touch();

            return Result.Success();
        }

        protected Result EnsureDeviceIsOn()
        {
            return Status == DeviceStatus.On
                ? Result.Success()
                : Result.Failure(LampErrors.AlreadyOn);
        }

        protected Result EnsureDeviceIsOff()
        {
            return Status == DeviceStatus.Off
                ? Result.Success()
                : Result.Failure(LampErrors.AlreadyOff);
        }

        protected void Touch()
        {
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}
using BlaisePascal.LessonsExamples.Domain.Devices;
using BlaisePascal.LessonsExamples.Domain.DoorDevice;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Doors
{
    public class Door : AbstractDevice
    {
        public DoorStatus DoorStatus { get; private set; }
        public LockStatus LockStatus { get; private set; }

        public Door(string name, string? imageUrl = null) : base(name, imageUrl)
        {
            Status = DeviceStatus.On; // A door is always "on" when created
            DoorStatus = DoorStatus.Closed;
            LockStatus = LockStatus.Locked;
        }

        public void Lock()
        {
            if (LockStatus == LockStatus.Locked)
                throw new InvalidOperationException($"{Name} is already locked.");

            if (DoorStatus != DoorStatus.Closed)
                throw new InvalidOperationException("You can lock the door only when fully closed.");

            LockStatus = LockStatus.Locked;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Unlock()
        {
            if (LockStatus == LockStatus.Unlocked)
                throw new InvalidOperationException($"{Name} is already unlocked.");

            LockStatus = LockStatus.Unlocked;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Open()
        {
            if (LockStatus == LockStatus.Locked)
                throw new InvalidOperationException("Cannot open a locked door.");

            DoorStatus = DoorStatus.Open;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Close()
        {
            DoorStatus = DoorStatus.Closed;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public override void SwitchOn()
        {
            // It depends on the business rules... here we assume we cannot enable a closed and locked door, but not mandatory
            if (DoorStatus == DoorStatus.Closed && LockStatus == LockStatus.Locked)
                throw new InvalidOperationException("Cannot enable an entirely locked door.");

            base.SwitchOn();
        }

        public override void SwitchOff()
        {
            // It depends on the business rules... here we assume we cannot disable an open door, but not mandatory
            if (DoorStatus != DoorStatus.Closed)
                throw new InvalidOperationException("Cannot disable the door while it's open.");

            base.SwitchOff();
        }
    }
}

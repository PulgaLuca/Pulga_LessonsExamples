using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.DoorDevice
{
    public sealed class Door : AbstractDevice, IDoor
    {
        public bool IsOpen => _DoorStatus == DoorStatus.Open;
        public bool IsLocked => _LockStatus == LockStatus.Locked;

        private DoorStatus _DoorStatus;
        private LockStatus _LockStatus;

        public Door(DeviceName name, DeviceImage? image = null) : base(name, image)
        {
            _DoorStatus = DoorStatus.Closed;
            _LockStatus = LockStatus.Locked;
        }

        public void Open()
        {
            EnsureUnlocked();

            if (IsOpen)
                throw new DomainException("Door is already open.");

            _DoorStatus = DoorStatus.Open;
            Touch();
        }

        public void Close()
        {
            if (!IsOpen)
                throw new DomainException("Door is already closed.");

            _DoorStatus = DoorStatus.Closed;
            Touch();
        }

        public void Lock()
        {
            EnsureClosed();

            if (IsLocked)
                throw new DomainException("Door is already locked.");

            _LockStatus = LockStatus.Locked;
            Touch();
        }

        public void Unlock()
        {
            if (!IsLocked)
                throw new DomainException("Door is already unlocked.");

            _LockStatus = LockStatus.Unlocked;
            Touch();
        }

        // ---- Guard methods ----

        private void EnsureClosed()
        {
            if (IsOpen)
                throw new DomainException("Door must be closed.");
        }

        private void EnsureUnlocked()
        {
            if (IsLocked)
                throw new DomainException("Door must be unlocked.");
        }
    }

}

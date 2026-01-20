namespace BlaisePascal.LessonsExamples.Domain.Devices.Shared
{
    public interface ILockable
    {
        bool IsLocked { get; }
        void Lock();
        void Unlock();
    }
}

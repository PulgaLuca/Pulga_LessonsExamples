namespace BlaisePascal.LessonsExamples.Domain.Devices.DoorDevice
{
    public interface IOpenable
    {
        bool IsOpen { get; }
        void Open();
        void Close();
    }
}

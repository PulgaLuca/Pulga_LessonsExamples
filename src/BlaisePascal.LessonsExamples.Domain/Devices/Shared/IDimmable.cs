namespace BlaisePascal.LessonsExamples.Domain.Devices.Shared
{
    public interface IDimmable
    {
        int BrightnessLevel { get; }
        void SetBrightness(int level);
    }
}

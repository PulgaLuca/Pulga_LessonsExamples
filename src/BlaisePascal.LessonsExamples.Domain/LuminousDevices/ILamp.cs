using BlaisePascal.LessonsExamples.Domain.Abstractions;
using BlaisePascal.LessonsExamples.Domain.LuminousDevices.ValueObjects;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public interface ILamp : IDevice
    {
        Brightness Intensity { get; }

        Brightness DefaultIntensity { get; }

        void SetIntensity(Brightness newIntensity);

        void Dimmer();
        void Dimmer(int amount);

        void Brighten();
        void Brighten(int amount);
    }
}

using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Luminous.ValueObjects;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Luminous
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

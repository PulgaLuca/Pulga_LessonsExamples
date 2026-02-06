using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public interface ILamp : IDevice
    {
        Brightness Brightness { get; }

        Brightness DefaultBrightness { get; }

        void ChangeBrightnessTo(int newBrightness);

        void Dimmer();
        void Dimmer(int amount);

        void Brighten();
        void Brighten(int amount);
    }
}

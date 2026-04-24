using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public interface ILamp : IDevice
    {
        Brightness Brightness { get; }

        Brightness DefaultBrightness { get; }

        Result ChangeBrightnessTo(int newBrightness);

        Result Dimmer();
        Result Dimmer(int amount);

        Result Brighten();
        Result Brighten(int amount);
    }
}
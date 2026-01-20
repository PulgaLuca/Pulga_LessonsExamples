using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Thermic
{
    public interface ITemperatureControlled : IDevice
    {
        Temperature TargetTemperature { get; }
        void ChangeTargetTemperature(Temperature temperature);
    }
}

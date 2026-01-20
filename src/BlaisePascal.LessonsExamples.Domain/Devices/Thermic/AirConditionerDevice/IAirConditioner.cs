using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Thermic.AirConditionerDevice
{
    public interface IAirConditionerDevice : IDevice
    {
        Temperature Temperature { get; }
        FanSpeed FanSpeed { get; }
        AcMode Mode { get; }

        void ChangeFanSpeed(FanSpeed speed);
        void ChangeMode(AcMode mode);
    }
}

using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Thermic.ThermostatDevice
{
    public interface IThermostatDevice : ITemperatureControlled
    {
        Temperature CurrentTemperature { get; }
        ThermostatMode Mode { get; }

        void ChangeMode(ThermostatMode mode);
    }
}

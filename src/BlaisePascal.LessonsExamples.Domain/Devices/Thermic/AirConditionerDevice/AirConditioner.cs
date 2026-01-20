using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Thermic.AirConditionerDevice
{
    public class AirConditioner : AbstractDevice, IAirConditionerDevice
    {
        public Temperature MinTemperature { get; private set; }
        public Temperature MaxTemperature { get; private set; }

        public Temperature Temperature { get; private set; }
        public Temperature TargetTemperature { get; private set; }

        public AcMode Mode { get; private set; }
        public FanSpeed FanSpeed { get; private set; }

        public TemperatureUnit TemperatureUnit => Temperature.Unit;

        public AirConditioner(
            DeviceName name,
            TemperatureUnit unit,
            double minTemp = 16,
            double maxTemp = 30,
            DeviceImage? imageUrl = null
        ) : base(name, imageUrl)
        {
            MinTemperature = new Temperature(minTemp, unit);
            MaxTemperature = new Temperature(maxTemp, unit);

            Temperature = MinTemperature; // default start temperature
        }

        //   TEMPERATURE MANAGEMENT
        public void ChangeTargetTemperature(Temperature newTemp)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot change temperature when AC is off.");

            // Ensure same unit
            Temperature tempInDeviceUnit = newTemp.ToUnit(TemperatureUnit);

            // Validate range
            if (tempInDeviceUnit.Value < MinTemperature.Value ||
                tempInDeviceUnit.Value > MaxTemperature.Value)
            {
                throw new ArgumentOutOfRangeException(nameof(newTemp), "Temperature out of allowed range.");
            }

            Temperature = tempInDeviceUnit;

            if (IsEcoModeActive())
                ApplyEcoModeRules();

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void ChangeTemperatureUnit(TemperatureUnit newUnit)
        {
            if (!Enum.IsDefined(typeof(TemperatureUnit), newUnit))
                throw new ArgumentException("Invalid temperature unit.", nameof(newUnit));

            if (TemperatureUnit == newUnit)
                return;

            // Convert all domain bounds and current temperature
            MinTemperature = MinTemperature.ToUnit(newUnit);
            MaxTemperature = MaxTemperature.ToUnit(newUnit);
            Temperature = Temperature.ToUnit(newUnit);

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        //   MODE & FAN
        public void ChangeFanSpeed(FanSpeed speed)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot change fan speed when AC is off.");

            if (!Enum.IsDefined(typeof(FanSpeed), speed))
                throw new ArgumentException("Invalid fan speed.", nameof(speed));

            FanSpeed = speed;

            if (IsEcoModeActive())
                ApplyEcoModeRules();

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void ChangeMode(AcMode mode)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot change mode when AC is off.");

            if (!Enum.IsDefined(typeof(AcMode), mode))
                throw new ArgumentException("Invalid AC mode.", nameof(mode));

            Mode = mode;

            if (IsEcoModeActive())
                ApplyEcoModeRules();

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        private bool IsEcoModeActive() => Mode == AcMode.EcoMode;

        private void ApplyEcoModeRules()
        {
            // Eco temperature band in Celsius
            Temperature ecoMin = Temperature.FromCelsius(20);
            Temperature ecoMax = Temperature.FromCelsius(26);

            Temperature = Temperature
                .ToUnit(TemperatureUnit)       // convert current temp to device unit
                .Clamp(ecoMin.ToUnit(TemperatureUnit),
                       ecoMax.ToUnit(TemperatureUnit));

            if (FanSpeed > FanSpeed.Medium)
                FanSpeed = FanSpeed.Medium;
        }
    }
}

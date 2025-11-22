using BlaisePascal.LessonsExamples.Domain.AirConditionerDevice;
using BlaisePascal.LessonsExamples.Domain.Devices;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Climate
{
    public class AirConditioner : AbstractDevice
    {
        // Temperature range configured by the domain
        public double MinTemperature { get; private set; }
        public double MaxTemperature { get; private set; }

        public double Temperature { get; private set; }
        public TemperatureUnit TemperatureUnit { get; private set; }

        public AcMode Mode { get; private set; }
        public FanSpeed FanSpeed { get; private set; }

        public AirConditioner(string name, TemperatureUnit temperatureUnit, string? imageUrl = null) : base(name, imageUrl)
        {
            TemperatureUnit = temperatureUnit;
        }

        public void SetTemperature(double value)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot change temperature when AC is off.");

            if (value < MinTemperature || value > MaxTemperature)
                throw new ArgumentOutOfRangeException(nameof(value), "Temperature out of allowed range.");

            Temperature = value;

            if (IsEcoModeActive())
                ApplyEcoModeRules();

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void SetFanSpeed(FanSpeed speed)
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

        public void SetMode(AcMode mode)
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

        public void SetTemperatureUnit(TemperatureUnit newUnit)
        {
            if (!Enum.IsDefined(typeof(TemperatureUnit), newUnit))
                throw new ArgumentException("Invalid temperature unit.", nameof(newUnit));

            if (TemperatureUnit == newUnit)
                return;

            Temperature = newUnit switch
            {
                TemperatureUnit.Celsius => (Temperature - 32) * 5 / 9,
                TemperatureUnit.Fahrenheit => (Temperature * 9 / 5) + 32,
                _ => Temperature
            };

            TemperatureUnit = newUnit;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        private bool IsEcoModeActive()
        {
            return Mode == AcMode.EcoMode;
        }

        private void ApplyEcoModeRules()
        {
            // Eco temperature band
            const double ecoMin = 20;
            const double ecoMax = 26;

            Temperature = Math.Clamp(Temperature, ecoMin, ecoMax);

            // Limit fan power
            if (FanSpeed > FanSpeed.Medium)
                FanSpeed = FanSpeed.Medium;
        }
    }
}

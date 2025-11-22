using BlaisePascal.LessonsExamples.Domain.Devices;
using BlaisePascal.LessonsExamples.Domain.ThermostatDevice;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Climate
{
    public class Thermostat : AbstractDevice
    {
        public double MinTemperature { get; private set; }
        public double MaxTemperature { get; private set; }

        public double TargetTemperature { get; private set; }
        public TemperatureUnit TemperatureUnit { get; private set; }
        public ThermostatMode Mode { get; private set; }

        public Thermostat(string name, TemperatureUnit temperatureUnit, string? imageUrl = null) : base(name, imageUrl)
        {
            TemperatureUnit = temperatureUnit;
        }

        public override void SwitchOff()
        {
            base.SwitchOff();
            Mode = ThermostatMode.Off;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void SetTargetTemperature(double value)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot set target temperature when thermostat is off.");

            if (value < MinTemperature || value > MaxTemperature)
                throw new ArgumentOutOfRangeException(nameof(value), "Target temperature out of range.");

            TargetTemperature = value;

            if (IsEcoModeActive())
                ApplyEcoModeRules();

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void SetMode(ThermostatMode mode)
        {
            if (!Enum.IsDefined(typeof(ThermostatMode), mode))
                throw new ArgumentException("Invalid mode.", nameof(mode));

            if (Status == DeviceStatus.Off && mode != ThermostatMode.Off)
                throw new InvalidOperationException("Cannot change thermostat mode while the device is off.");

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

            TargetTemperature = newUnit switch
            {
                TemperatureUnit.Celsius => (TargetTemperature - 32) * 5 / 9,
                TemperatureUnit.Fahrenheit => (TargetTemperature * 9 / 5) + 32,
                _ => TargetTemperature
            };

            TemperatureUnit = newUnit;

            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        private bool IsEcoModeActive()
        {
            return Mode == ThermostatMode.EcoMode;
        }

        private void ApplyEcoModeRules()
        {
            // Eco recommended band: 19–24 °C
            const double ecoMin = 19;
            const double ecoMax = 24;

            TargetTemperature = Math.Clamp(TargetTemperature, ecoMin, ecoMax);
        }
    }
}

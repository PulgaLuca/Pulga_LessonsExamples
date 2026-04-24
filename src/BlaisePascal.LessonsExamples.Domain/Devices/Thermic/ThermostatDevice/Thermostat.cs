//using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
//using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
//using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;

//namespace BlaisePascal.LessonsExamples.Domain.Devices.Thermic.ThermostatDevice
//{
//    public sealed class Thermostat : AbstractDevice, IThermostatDevice
//    {
//        private readonly double _minCelsius;
//        private readonly double _maxCelsius;

//        public Temperature TargetTemperature { get; private set; }
//        public Temperature CurrentTemperature { get; private set; }
//        public ThermostatMode Mode { get; private set; }

//        public Thermostat(
//            DeviceName name,
//            Temperature initialTarget,
//            double minCelsius,
//            double maxCelsius,
//            DeviceImage? image = null)
//            : base(name, image)
//        {
//            _minCelsius = minCelsius;
//            _maxCelsius = maxCelsius;

//            TargetTemperature = initialTarget;
//            Mode = ThermostatMode.Off;
//        }

//        public override void SwitchOff()
//        {
//            base.SwitchOff();
//            Mode = ThermostatMode.Off;
//            Touch();
//        }

//        public void ChangeTargetTemperature(Temperature temperature)
//        {
//            EnsureIsOn();

//            var normalized = temperature
//                .ToUnit(TemperatureUnit.Celsius)
//                .Clamp(_minCelsius, _maxCelsius);

//            TargetTemperature = IsEcoModeActive()
//                ? ApplyEcoRules(normalized)
//                : normalized;

//            Touch();
//        }

//        public void ChangeMode(ThermostatMode mode)
//        {
//            if (!Enum.IsDefined(typeof(ThermostatMode), mode))
//                throw new ArgumentException("Invalid thermostat mode.", nameof(mode));

//            EnsureIsOnOrOffMode(mode);

//            Mode = mode;

//            if (IsEcoModeActive())
//                TargetTemperature = ApplyEcoRules(TargetTemperature);

//            Touch();
//        }

//        private bool IsEcoModeActive() => Mode == ThermostatMode.EcoMode;

//        private static Temperature ApplyEcoRules(Temperature temperature)
//        {
//            const double ecoMin = 19;
//            const double ecoMax = 24;

//            return temperature
//                .ToUnit(TemperatureUnit.Celsius)
//                .Clamp(ecoMin, ecoMax);
//        }

//        private void EnsureIsOn()
//        {
//            if (Status == DeviceStatus.Off)
//                throw new InvalidOperationException("Thermostat is off.");
//        }

//        private void EnsureIsOnOrOffMode(ThermostatMode mode)
//        {
//            if (Status == DeviceStatus.Off && mode != ThermostatMode.Off)
//                throw new InvalidOperationException("Cannot change mode while device is off.");
//        }
//    }
//}

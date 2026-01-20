namespace BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO
{
    public sealed class Temperature
    {
        public double Value { get; }
        public TemperatureUnit Unit { get; }

        public Temperature(double value, TemperatureUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public static Temperature FromCelsius(double c) =>
            new(c, TemperatureUnit.Celsius);

        public static Temperature FromFahrenheit(double f) =>
            new(f, TemperatureUnit.Fahrenheit);

        public Temperature ToUnit(TemperatureUnit newUnit)
        {
            if (Unit == newUnit) return this;

            return newUnit switch
            {
                TemperatureUnit.Celsius =>
                    new Temperature((Value - 32) * 5 / 9, TemperatureUnit.Celsius),

                TemperatureUnit.Fahrenheit =>
                    new Temperature(Value * 9 / 5 + 32, TemperatureUnit.Fahrenheit),

                _ => throw new NotSupportedException()
            };
        }

        public Temperature Clamp(Temperature min, Temperature max)
        {
            double vInMinUnit = ToUnit(min.Unit).Value;

            double clamped = Math.Clamp(vInMinUnit, min.Value, max.Value);

            return new Temperature(clamped, min.Unit).ToUnit(Unit);
        }

        public Temperature Clamp(double min, double max) => new(Math.Clamp(Value, min, max), Unit);

        public override string ToString() => $"{Value:0.0}°{(Unit == TemperatureUnit.Celsius ? "C" : "F")}";
    }
}

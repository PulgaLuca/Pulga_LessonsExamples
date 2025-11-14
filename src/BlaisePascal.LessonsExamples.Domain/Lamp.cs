using System;

namespace BlaisePascal.LessonsExamples.Domain
{
    public class Lamp : AbstractLamp
    {
        private const int StandardMinIntensity = 0;
        private const int StandardDefaultIntensity = 50;
        private const int StandardMaxIntensity = 100;

        public Lamp(string name)
            : base(name)
        {
        }

        public override void SwitchOn()
        {
            if (Status == DeviceStatus.On)
                throw new InvalidOperationException("La lampada è già accesa.");

            Status = DeviceStatus.On;
            Intensity = StandardDefaultIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public override void SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("La lampada è già spenta.");

            Status = DeviceStatus.Off;
            Intensity = StandardMinIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public override void SetIntensity(int value)
        {
            if (value < StandardMinIntensity || value > StandardMaxIntensity)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"L’intensità deve essere compresa tra {StandardMinIntensity} e {StandardMaxIntensity}.");

            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Non è possibile modificare l’intensità di una lampada spenta.");

            Intensity = value;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public int GetStandardMinIntensity() => StandardMinIntensity;
        public int GetStandardMaxIntensity() => StandardMaxIntensity;
        public int GetStandardDefaultIntensity() => StandardDefaultIntensity;
    }
}

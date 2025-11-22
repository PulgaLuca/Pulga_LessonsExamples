using System;
using BlaisePascal.LessonsExamples.Domain.Devices;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public abstract class AbstractLamp : AbstractDevice
    {
        public int Intensity { get; protected set; }

        // Values depend on the specific lamp implementation (Lamp, EcoLamp in out case)
        public abstract int MinIntensity { get; }
        public abstract int MaxIntensity { get; }
        public abstract int DefaultIntensity { get; }

        private const int DefaultStepAmount = 10;

        protected AbstractLamp(string name, string? imageUrl = null) : base(name, imageUrl)
        {
            Intensity = MinIntensity;
        }

        public override void SwitchOn()
        {
            base.SwitchOn();
            Intensity = DefaultIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public override void SwitchOff()
        {
            base.SwitchOff();
            Intensity = MinIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void SetIntensity(int newIntensity)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("It is not possible to change intensity when the lamp is off.");

            Intensity = Math.Clamp(newIntensity, MinIntensity, MaxIntensity);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void Dimmer()
        {
            Dimmer(DefaultStepAmount);
        }

        public virtual void Dimmer(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("It is not possible to dim a lamp that is off.");

            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "The change must be at least 1.");

            Intensity = Math.Max(MinIntensity, Intensity - amount);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void Brighten()
        {
            Brighten(DefaultStepAmount);
        }

        public virtual void Brighten(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("It is not possible to increase the intensity of a lamp that is off.");

            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "The change must be at least 1.");

            Intensity = Math.Min(MaxIntensity, Intensity + amount);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}

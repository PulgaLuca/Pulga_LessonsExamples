using BlaisePascal.LessonsExamples.Domain.LuminousDevices.ValueObjects;
using BlaisePascal.LessonsExamples.Domain.Shared;
using System;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public abstract class AbstractLamp : AbstractDevice, ILamp
    {
        public Brightness Intensity { get; protected set; }

        public abstract Brightness DefaultIntensity { get; }

        private const int DefaultStepAmount = 10;

        protected AbstractLamp(string name, string? imageUrl = null) : base(name, imageUrl)
        {
            Intensity = Brightness.From(Brightness.Min);
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
            Intensity = Brightness.From(Brightness.Min);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void SetIntensity(Brightness newIntensity)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot change intensity when lamp is off.");

            Intensity = newIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Dimmer() => Dimmer(DefaultStepAmount);

        public virtual void Dimmer(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Lamp is off.");

            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Intensity = Intensity - amount;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Brighten() => Brighten(DefaultStepAmount);

        public virtual void Brighten(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Lamp is off.");

            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Intensity = Intensity + amount;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}

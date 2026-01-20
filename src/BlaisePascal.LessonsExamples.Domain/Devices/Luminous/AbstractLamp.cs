using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Luminous.ValueObjects;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Luminous
{
    public abstract class AbstractLamp : AbstractDevice, ILamp
    {
        public Brightness Intensity { get; protected set; }

        public abstract Brightness DefaultIntensity { get; }

        private const int DefaultStepAmount = 10;

        protected AbstractLamp(DeviceName name, DeviceImage? imageUrl = null) : base(name, imageUrl)
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

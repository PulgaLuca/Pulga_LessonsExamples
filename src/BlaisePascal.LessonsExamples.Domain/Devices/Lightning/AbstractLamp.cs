using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public abstract class AbstractLamp : AbstractDevice, ILamp
    {
        public Brightness Brightness { get; set; }

        public abstract Brightness DefaultBrightness { get; }

        public AbstractLamp(DeviceName name, DeviceImage imageUrl) : base(name, imageUrl)
        {
            Brightness = Brightness.From(Brightness.Min);
        }

        public AbstractLamp(Guid id, DeviceName name, DeviceImage imageUrl, DeviceStatus status, Brightness brightness, DateTime createdAtUtc, DateTime lastModifiedAtUtc) : base(name, imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Status = status;
            Brightness = brightness;
            CreatedAtUtc = createdAtUtc;
            LastModifiedAtUtc = lastModifiedAtUtc;
        }

        public override void SwitchOn()
        {
            base.SwitchOn();
            Brightness = DefaultBrightness;
            Touch();
        }

        public override void SwitchOff()
        {
            base.SwitchOff();
            Brightness = Brightness.From(Brightness.Min);
            Touch();
        }

        public virtual void ChangeBrightnessTo(int newIntensity)
        {
            CheckLampOff();

            Brightness = Brightness.From(newIntensity);
            Touch();
        }

        public void Dimmer() => Dimmer(Brightness.DefaultStepAmount);

        public virtual void Dimmer(int amount)
        {
            CheckLampOff();

            Brightness = Brightness.From(Brightness - amount);
            Touch();
        }

        public void Brighten() => Brighten(Brightness.DefaultStepAmount);

        public virtual void Brighten(int amount)
        {
            CheckLampOff();

            Brightness = Brightness.From(Brightness + amount);
            Touch();
        }

        private void CheckLampOff()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Lamp is off.");
        }

        private void CheckLampOn()
        {
            if (Status == DeviceStatus.On)
                throw new InvalidOperationException("Lamp is on.");
        }
    }
}

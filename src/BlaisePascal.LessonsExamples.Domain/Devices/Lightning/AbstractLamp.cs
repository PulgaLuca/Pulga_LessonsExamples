using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public abstract class AbstractLamp : AbstractDevice, ILamp
    {
        public Brightness Brightness { get; set; }

        public abstract Brightness DefaultBrightness { get; }

        protected AbstractLamp(DeviceName name, DeviceImage imageUrl) : base(name, imageUrl)
        {
            Brightness = Brightness.From(Brightness.Min);
        }

        public override void SwitchOn()
        {
            base.SwitchOn();
            Brightness = DefaultBrightness;

            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();
        }

        public override void SwitchOff()
        {
            base.SwitchOff();
            Brightness = Brightness.From(Brightness.Min);

            Raise(new DeviceSwitchedOffEvent(Id));
            Touch();
        }

        public virtual void ChangeBrightnessTo(int newIntensity)
        {
            EnsureIsOn();

            Brightness oldBrightness = Brightness;
            Brightness = Brightness.From(newIntensity);

            Raise(new BrightnessChangedEvent(Id, oldBrightness, Brightness));
            Touch();
        }

        public void Dimmer() => Dimmer(Brightness.DefaultStepAmount);

        public virtual void Dimmer(int amount)
        {
            EnsureIsOn();

            Brightness oldBrightness = Brightness;
            Brightness = Brightness.Decrease(amount);

            Raise(new BrightnessChangedEvent(Id, oldBrightness, Brightness));
            Touch();
        }

        public void Brighten() => Brighten(Brightness.DefaultStepAmount);

        public virtual void Brighten(int amount)
        {
            EnsureIsOn();

            Brightness oldBrightness = Brightness;
            Brightness = Brightness.Increase(amount);

            Raise(new BrightnessChangedEvent(Id, oldBrightness, Brightness));
            Touch();
        }

        private void EnsureIsOn()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Lamp is off.");
        }
    }
}

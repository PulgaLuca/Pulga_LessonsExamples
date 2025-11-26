using BlaisePascal.LessonsExamples.Domain.LuminousDevices.ValueObjects;
using BlaisePascal.LessonsExamples.Domain.Shared;
using System;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public class EcoLamp : AbstractLamp
    {
        public override Brightness DefaultIntensity => Brightness.From(30);

        private const int DefaultAutoOffMinutes = 10;
        private const int MinAutoOffMinutes = 1;

        private DateTime? autoOffAtUtc;

        public EcoLamp(string name) : base(name)
        {
        }

        public override void SwitchOn()
        {
            SwitchOn(enableAutoOff: false);
        }

        public void SwitchOn(bool enableAutoOff)
        {
            base.SwitchOn();
            autoOffAtUtc = enableAutoOff
                ? DateTime.UtcNow.AddMinutes(DefaultAutoOffMinutes)
                : null;
        }

        public void SwitchOn(int autoOffMinutes)
        {
            if (autoOffMinutes < MinAutoOffMinutes)
                throw new ArgumentOutOfRangeException(nameof(autoOffMinutes));

            base.SwitchOn();
            autoOffAtUtc = DateTime.UtcNow.AddMinutes(autoOffMinutes);
        }

        public override void SetIntensity(Brightness value)
        {
            base.SetIntensity(value);
            ResetAutoOffIfNeeded();
        }

        public override void Dimmer(int amount)
        {
            base.Dimmer(amount);
            ResetAutoOffIfNeeded();
        }

        public override void Brighten(int amount)
        {
            base.Brighten(amount);
            ResetAutoOffIfNeeded();
        }

        public override void SwitchOff()
        {
            base.SwitchOff();
            autoOffAtUtc = null;
        }

        public void CheckAutoOff()
        {
            if (Status == DeviceStatus.On &&
                autoOffAtUtc.HasValue &&
                DateTime.UtcNow >= autoOffAtUtc.Value)
            {
                SwitchOff();
            }
        }

        private void ResetAutoOffIfNeeded()
        {
            if (autoOffAtUtc.HasValue)
                autoOffAtUtc = DateTime.UtcNow.AddMinutes(DefaultAutoOffMinutes);
        }
    }
}

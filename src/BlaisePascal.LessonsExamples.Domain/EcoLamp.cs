using System;

namespace BlaisePascal.LessonsExamples.Domain
{
    public class EcoLamp : AbstractLamp
    {
        private const int EcoMin = 0;
        private const int EcoDefault = 30;
        private const int EcoMax = 70;
        private const int DefaultAutoOffMinutes = 10;
        private const int MinAutoOffMinutes = 1;

        private DateTime? autoOffAtUtc;

        public EcoLamp(string name) : base(name) { }

        public override int MinIntensity => EcoMin;
        public override int MaxIntensity => EcoMax;
        public override int DefaultIntensity => EcoDefault;

        /// <summary>
        /// Accende la lampada senza auto-off
        /// </summary>
        public override void SwitchOn()
        {
            // SwitchOn(false);
            SwitchOn(enableAutoOff: false);
        }

        /// <summary>
        /// Accensione con scelta auto-off on/off
        /// </summary>
        public void SwitchOn(bool enableAutoOff)
        {
            base.SwitchOn();

            autoOffAtUtc = enableAutoOff ? DateTime.UtcNow.AddMinutes(DefaultAutoOffMinutes) : null;
        }

        /// <summary>
        /// Accende la lampada impostando un auto-off personalizzato
        /// </summary>
        public void SwitchOn(int autoOffMinutes)
        {
            if (autoOffMinutes < MinAutoOffMinutes)
                throw new ArgumentOutOfRangeException($"Il tempo minimo di auto-spegnimento è {MinAutoOffMinutes} minuto.");

            base.SwitchOn();
            autoOffAtUtc = DateTime.UtcNow.AddMinutes(autoOffMinutes);
        }

        // Override per gestione intensità ed eventuale reset del timer
        public override void SetIntensity(int value)
        {
            base.SetIntensity(value);

            if (autoOffAtUtc.HasValue)
                autoOffAtUtc = DateTime.UtcNow.AddMinutes(DefaultAutoOffMinutes);
        }

        public override void SwitchOff()
        {
            base.SwitchOff();
            autoOffAtUtc = null;
        }

        public void CheckAutoOff()
        {
            if (Status == DeviceStatus.On && autoOffAtUtc.HasValue && DateTime.UtcNow >= autoOffAtUtc.Value)
                SwitchOff();
        }

        public override void Dimmer()
        {
            base.Dimmer();
            ResetAutoOffIfNeeded();
        }

        public override void Dimmer(int amount)
        {
            base.Dimmer(amount);
            ResetAutoOffIfNeeded();
        }

        public override void Brighten()
        {
            base.Brighten();
            ResetAutoOffIfNeeded();
        }

        public override void Brighten(int amount)
        {
            base.Brighten(amount);
            ResetAutoOffIfNeeded();
        }

        private void ResetAutoOffIfNeeded()
        {
            if (autoOffAtUtc.HasValue)
                autoOffAtUtc = DateTime.UtcNow.AddMinutes(DefaultAutoOffMinutes);
        }
    }
}

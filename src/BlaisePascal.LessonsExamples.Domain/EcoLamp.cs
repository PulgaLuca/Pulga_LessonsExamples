using System;

namespace BlaisePascal.LessonsExamples.Domain
{
    /// <summary>
    /// Lampada con modalità risparmio energetico (Eco Mode).
    /// </summary>
    public class EcoLamp : AbstractLamp
    {
        private const int EcoMinIntensity = 0;
        private const int EcoDefaultIntensity = 30;
        private const int EcoMaxIntensity = 70;

        private DateTime? AutoOffAtUtc;

        public EcoLamp(string name)
            : base(name)
        {
        }

        // --- Implementazione Metodi Astratti ---

        public override void SwitchOn()
        {
            if (Status == DeviceStatus.On)
                throw new InvalidOperationException("La EcoLamp è già accesa.");

            Status = DeviceStatus.On;
            Intensity = EcoDefaultIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;

            // Pianifica lo spegnimento automatico dopo 10 minuti
            AutoOffAtUtc = DateTime.UtcNow.AddMinutes(10);
        }

        public override void SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("La EcoLamp è già spenta.");

            Status = DeviceStatus.Off;
            Intensity = EcoMinIntensity;
            AutoOffAtUtc = null;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public override void SetIntensity(int value)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Non è possibile impostare l’intensità di una lampada spenta.");

            if (value < EcoMinIntensity || value > EcoMaxIntensity)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"L’intensità deve essere compresa tra {EcoMinIntensity} e {EcoMaxIntensity} in modalità Eco.");

            Intensity = value;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        // --- Funzionalità Specifica della EcoLamp ---

        /// <summary>
        /// Verifica se la lampada deve spegnersi automaticamente per risparmio energetico.
        /// </summary>
        public void CheckAutoOff()
        {
            if (Status == DeviceStatus.On && AutoOffAtUtc.HasValue && DateTime.UtcNow >= AutoOffAtUtc.Value)
            {
                SwitchOff();
                AutoOffAtUtc = null;
            }
        }

        public int GetEcoMinIntensity() => EcoMinIntensity;
        public int GetEcoMaxIntensity() => EcoMaxIntensity;
        public int GetEcoDefaultIntensity() => EcoDefaultIntensity;
    }
}

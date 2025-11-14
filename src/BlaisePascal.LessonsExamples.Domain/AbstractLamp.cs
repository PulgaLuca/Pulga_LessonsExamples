using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain
{
    /// <summary>
    /// Classe astratta base per tutte le lampade del dominio.
    /// Definisce i comportamenti comuni e i contratti che le sottoclassi devono rispettare.
    /// </summary>
    public abstract class AbstractLamp
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public int Intensity { get; protected set; }
        public DeviceStatus Status { get; protected set; }
        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime LastModifiedAtUtc { get; protected set; }

        protected AbstractLamp(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Intensity = 0;
            Status = DeviceStatus.Off;
            CreatedAtUtc = DateTime.UtcNow;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        // --- Metodi astratti (devono essere implementati dalle sottoclassi che specializzano) ---
        public abstract void SwitchOn();
        public abstract void SwitchOff();
        public abstract void SetIntensity(int value);

        // --- Metodi concreti comuni ---
        public void Toggle()
        {
            if (Status == DeviceStatus.On)
                SwitchOff();
            else
                SwitchOn();

            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Dimmer(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Impossibile regolare l’intensità: la lampada è spenta.");

            int newValue = Math.Max(0, Intensity - amount);
            if (newValue == Intensity)
                throw new InvalidOperationException("L’intensità non può essere ulteriormente diminuita.");

            Intensity = newValue;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Brighten(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Impossibile regolare l’intensità: la lampada è spenta.");

            int newValue = Math.Min(100, Intensity + amount);
            if (newValue == Intensity)
                throw new InvalidOperationException("L’intensità non può essere ulteriormente aumentata.");

            Intensity = newValue;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}
using System;

namespace BlaisePascal.LessonsExamples.Domain
{
    public abstract class AbstractLamp
    {
        public Guid Id { get; }
        public string Name { get; protected set; }
        public DeviceStatus Status { get; protected set; }
        public int Intensity { get; protected set; }
        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime LastModifiedAtUtc { get; protected set; }

        // Proprietà definite dalle classi derivate
        public abstract int MinIntensity { get; }
        public abstract int MaxIntensity { get; }
        public abstract int DefaultIntensity { get; }

        private const int DefaultStepAmount = 10;

        protected AbstractLamp(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAtUtc = DateTime.UtcNow;
        }
        
        // Metodi comuni e virtual modificabili
        public virtual void SwitchOn()
        {
            if (Status == DeviceStatus.On)
                throw new InvalidOperationException($"{Name} è già accesa.");

            Status = DeviceStatus.On;
            Intensity = DefaultIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException($"{Name} è già spenta.");

            Status = DeviceStatus.Off;
            Intensity = MinIntensity;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void SetIntensity(int value)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Non è possibile cambiare intensità quando la lampada è spenta.");

            value = Math.Clamp(value, MinIntensity, MaxIntensity);

            Intensity = value;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public virtual void Dimmer()
        {
            Dimmer(DefaultStepAmount);
        }

        public virtual void Dimmer(int amount)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Non è possibile diminuire l'intensità di una lampada spenta.");

            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "La variazione deve essere almeno 1.");

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
                throw new InvalidOperationException("Non è possibile aumentare l'intensità di una lampada spenta.");

            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "La variazione deve essere almeno 1.");

            Intensity = Math.Min(MaxIntensity, Intensity + amount);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}

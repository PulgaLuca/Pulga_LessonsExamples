using System;

namespace BlaisePascal.LessonsExamples.Domain
{
    public class TwoLampsDevice
    {
        public AbstractLamp Lamp1 { get; private set; }
        public AbstractLamp Lamp2 { get; private set; }

        public TwoLampsDevice(AbstractLamp lamp1, AbstractLamp lamp2)
        {
            Lamp1 = lamp1 ?? throw new ArgumentNullException(nameof(lamp1));
            Lamp2 = lamp2 ?? throw new ArgumentNullException(nameof(lamp2));
        }

        // --- SWITCH ON ---
        public void SwitchOn()
        {
            Lamp1.SwitchOn();
            Lamp2.SwitchOn();
        }

        public void SwitchOn(int lampNumber)
        {
            GetLamp(lampNumber).SwitchOn();
        }

        // --- SWITCH OFF ---
        public void SwitchOff()
        {
            Lamp1.SwitchOff();
            Lamp2.SwitchOff();
        }

        public void SwitchOff(int lampNumber)
        {
            GetLamp(lampNumber).SwitchOff();
        }

        // --- TOGGLE ---
        public void Toggle()
        {
            Lamp1.Toggle();
            Lamp2.Toggle();
        }

        public void Toggle(int lampNumber)
        {
            GetLamp(lampNumber).Toggle();
        }

        // --- SET INTENSITY ---
        public void SetIntensity(int value)
        {
            Lamp1.SetIntensity(value);
            Lamp2.SetIntensity(value);
        }

        public void SetIntensity(int lampNumber, int value)
        {
            GetLamp(lampNumber).SetIntensity(value);
        }

        // --- DIMMER ---
        public void Dimmer(int amount = 10)
        {
            Lamp1.Dimmer(amount);
            Lamp2.Dimmer(amount);
        }

        public void Dimmer(int lampNumber, int amount = 10)
        {
            GetLamp(lampNumber).Dimmer(amount);
        }

        // --- BRIGHTEN ---
        public void Brighten(int amount = 10)
        {
            Lamp1.Brighten(amount);
            Lamp2.Brighten(amount);
        }

        public void Brighten(int lampNumber, int amount = 10)
        {
            GetLamp(lampNumber).Brighten(amount);
        }

        // --- AUTO OFF CHECK ---
        public void CheckAutoOff()
        {
            if (Lamp1 is EcoLamp eco1)
                eco1.CheckAutoOff();

            if (Lamp2 is EcoLamp eco2)
                eco2.CheckAutoOff();
        }

        // --- PRIVATE UTILITIES ---
        private AbstractLamp GetLamp(int number)
        {
            return number switch
            {
                1 => Lamp1,
                2 => Lamp2,
                _ => throw new ArgumentOutOfRangeException(nameof(number), "Solo 1 o 2 sono ammessi.")
            };
        }
    }
}

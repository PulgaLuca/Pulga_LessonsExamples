using System;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public class TwoLampsDevice
    {
        private const int Lamp1Number = 1;
        private const int Lamp2Number = 2;

        private const int DefaultStepAmount = 10;

        public AbstractLamp Lamp1 { get; }
        public AbstractLamp Lamp2 { get; }

        public TwoLampsDevice(AbstractLamp lamp1, AbstractLamp lamp2)
        {
            Lamp1 = lamp1 ?? throw new ArgumentNullException(nameof(lamp1));
            Lamp2 = lamp2 ?? throw new ArgumentNullException(nameof(lamp2));
        }

        // ------- Switch On ------- 
        public void SwitchOn()
        {
            Lamp1.SwitchOn();
            Lamp2.SwitchOn();
        }

        public void SwitchOn(int lampNumber)
        {
            GetLamp(lampNumber).SwitchOn();
        }

        public void SwitchOn(Guid lampId)
        {
            GetLamp(lampId).SwitchOn();
        }

        // ------- Switch Off -------
        public void SwitchOff()
        {
            Lamp1.SwitchOff();
            Lamp2.SwitchOff();
        }

        public void SwitchOff(int lampNumber)
        {
            GetLamp(lampNumber).SwitchOff();
        }

        public void SwitchOff(Guid lampId)
        {
            GetLamp(lampId).SwitchOff();
        }

        // ------- Set Intensity -------
        public void SetIntensity(int newIntensity)
        {
            Lamp1.SetIntensity(newIntensity);
            Lamp2.SetIntensity(newIntensity);
        }

        public void SetIntensity(int lampNumber, int newIntensity)
        {
            GetLamp(lampNumber).SetIntensity(newIntensity);
        }

        public void SetIntensity(Guid lampId, int newIntensity)
        {
            GetLamp(lampId).SetIntensity(newIntensity);
        }

        // ------- Dimmer -------
        public void Dimmer(int amount = DefaultStepAmount)
        {
            Lamp1.Dimmer(amount);
            Lamp2.Dimmer(amount);
        }

        public void Dimmer(int lampNumber, int amount)
        {
            GetLamp(lampNumber).Dimmer(amount);
        }

        public void Dimmer(Guid lampId, int amount)
        {
            GetLamp(lampId).Dimmer(amount);
        }

        // ------- Brighten -------
        public void Brighten(int amount = DefaultStepAmount)
        {
            Lamp1.Brighten(amount);
            Lamp2.Brighten(amount);
        }

        public void Brighten(int lampNumber, int amount)
        {
            GetLamp(lampNumber).Brighten(amount);
        }

        public void Brighten(Guid lampId, int amount)
        {
            GetLamp(lampId).Brighten(amount);
        }

        public void CheckAutoOff()
        {
            if (Lamp1 is EcoLamp eco1) 
                eco1.CheckAutoOff();
            if (Lamp2 is EcoLamp eco2) 
                eco2.CheckAutoOff();
        }

        // ------- Get Lamp Utilities methods -------
        private AbstractLamp GetLamp(int lampNumber)
        {
            switch (lampNumber)
            {
                case Lamp1Number:
                    return Lamp1;
                case Lamp2Number:
                    return Lamp2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lampNumber), "Numero lampada non valido.");
            }
        }

        private AbstractLamp GetLamp(Guid lampId)
        {
            if (Lamp1.Id == lampId)
                return Lamp1;
            if (Lamp2.Id == lampId)
                return Lamp2;
            throw new ArgumentOutOfRangeException(nameof(lampId), "ID lampada non valido.");
        }
    }
}

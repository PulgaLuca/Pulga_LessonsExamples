using System;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public class Lamp : AbstractLamp
    {
        private const int StandardMin = 0;
        private const int StandardDefault = 50;
        private const int StandardMax = 100;

        public Lamp(string name) : base(name) { }

        public override int MinIntensity => StandardMin;
        public override int MaxIntensity => StandardMax;
        public override int DefaultIntensity => StandardDefault;
    }
}

using BlaisePascal.LessonsExamples.Domain.LuminousDevices.ValueObjects;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public class Lamp : AbstractLamp
    {
        public Lamp(string name) : base(name) { }

        public override Brightness DefaultIntensity => Brightness.Medium();
    }
}

using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Luminous.ValueObjects;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Luminous
{
    public class Lamp : AbstractLamp
    {
        public Lamp(DeviceName name) : base(name) { }

        public override Brightness DefaultIntensity => Brightness.Medium();

        public Brightness Brightness { get; private set; }

        public void change(int intensity)
        {
            Brightness = Brightness.From(intensity);
        }
    }
}

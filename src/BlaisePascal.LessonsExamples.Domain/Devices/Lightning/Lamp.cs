using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public class Lamp : AbstractLamp
    {
        public Lamp(DeviceName name, DeviceImage image) : base(name, image) { }

        public Lamp(Guid id, DeviceName name, DeviceImage imageUrl, DeviceStatus status, Brightness brightness, DateTime createdAtUtc, DateTime lastModifiedAtUtc) 
            : base(name, imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Status = status;
            Brightness = brightness;
            CreatedAtUtc = createdAtUtc;
            LastModifiedAtUtc = lastModifiedAtUtc;
        }

        public override Brightness DefaultBrightness => Brightness.Medium();
    }
}

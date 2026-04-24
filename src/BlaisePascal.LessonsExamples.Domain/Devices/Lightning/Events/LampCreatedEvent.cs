using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Events
{
    public sealed class LampCreatedEvent(Guid LampId, DeviceName Name) : DomainEvent;
}

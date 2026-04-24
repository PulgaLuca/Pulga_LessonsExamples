using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using BlaisePascal.LessonsExamples.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Events
{
    public sealed class BrightnessChangedEvent(Guid LampId, Brightness OldValue, Brightness NewValue) : DomainEvent;
}

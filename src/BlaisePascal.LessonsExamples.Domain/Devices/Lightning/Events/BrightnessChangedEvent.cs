using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using BlaisePascal.LessonsExamples.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Events
{
    public sealed class BrightnessChangedEvent : DomainEvent
    {
        public Guid LampId { get; }
        public Brightness OldValue { get; }
        public Brightness NewValue { get; }

        public BrightnessChangedEvent(Guid lampId, Brightness oldValue, Brightness newValue)
        {
            LampId = lampId;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}

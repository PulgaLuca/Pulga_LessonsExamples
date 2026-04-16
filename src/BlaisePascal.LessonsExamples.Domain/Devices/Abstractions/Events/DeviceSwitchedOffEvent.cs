using BlaisePascal.LessonsExamples.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Events
{
    public sealed class DeviceSwitchedOffEvent : DomainEvent
    {
        public Guid DeviceId { get; }

        public DeviceSwitchedOffEvent(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}

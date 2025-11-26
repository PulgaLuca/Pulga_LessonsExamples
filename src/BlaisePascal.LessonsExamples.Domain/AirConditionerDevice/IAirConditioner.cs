using BlaisePascal.LessonsExamples.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.AirConditionerDevice
{
    public interface IAirConditionerDevice : IDevice
    {
        int FanSpeed { get; }

        void SetFanSpeed(int speed);
    }
}

using BlaisePascal.LessonsExamples.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.ThermostatDevice
{
    public interface IThermostatDevice : IDevice
    {
        double TargetTemperature { get; }
        double CurrentTemperature { get; }

        void SetTargetTemperature(double temperature);
    }
}

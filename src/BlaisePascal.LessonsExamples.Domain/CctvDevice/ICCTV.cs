using BlaisePascal.LessonsExamples.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.CctvDevice
{
    public interface ICCTV : IDevice
    {
        bool IsRecording { get; }

        void StartRecording();
        void StopRecording();
    }
}

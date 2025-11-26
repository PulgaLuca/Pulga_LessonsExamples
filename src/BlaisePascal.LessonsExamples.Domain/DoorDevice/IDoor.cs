using BlaisePascal.LessonsExamples.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.DoorDevice
{
    public interface IDoor : IDevice
    {
        bool IsLocked { get; }

        void Lock();
        void Unlock();
    }
}

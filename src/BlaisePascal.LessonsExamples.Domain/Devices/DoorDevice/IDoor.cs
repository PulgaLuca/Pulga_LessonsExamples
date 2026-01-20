using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.Shared;

namespace BlaisePascal.LessonsExamples.Domain.Devices.DoorDevice
{
    public interface IDoor : IDevice, IOpenable, ILockable
    {

    }
}

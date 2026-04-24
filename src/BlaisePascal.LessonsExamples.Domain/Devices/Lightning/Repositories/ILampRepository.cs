using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories
{
    public interface ILampRepository
    {
        Result Add(Lamp lamp);
        Result Update(Lamp lamp);
        Result Remove(Guid id);

        Result<Lamp> GetById(Guid id);
        Result<List<Lamp>> GetAll();
    }
}

using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.SharedKernel;
using System.Text.Json;

namespace BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Json.Devices.Lightning
{
    public class JsonLampRepository : ILampRepository
    {
        private readonly string _filePath;

        public JsonLampRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();

            var dataFolder = Path.Combine(solutionRoot, "data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "lamps.json");

            if (!File.Exists(_filePath))
            {
                Save(new List<Lamp>());
            }
        }

        public Result<List<Lamp>> GetAll()
        {
            try
            {
                return Result.Success(Load());
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Lamp>>(Error.NotFound("[LAMP REPOSITORY]", ex.Message));
            }
        }

        public Result<Lamp> GetById(Guid id)
        {
            try
            {
                var lamps = Load();

                var lamp = lamps.FirstOrDefault(l => l.Id == id);

                if (lamp is null)
                    return Result.Failure<Lamp>(Error.NotFound("[LAMP REPOSITORY]", "Lamp not found"));

                return Result.Success(lamp);
            }
            catch (Exception ex)
            {
                return Result.Failure<Lamp>(Error.NotFound("[LAMP REPOSITORY]", ex.Message));
            }
        }

        public Result Add(Lamp lamp)
        {
            try
            {
                var lampsResult = GetAll();
                if (lampsResult.IsFailure)
                    return Result.Failure(lampsResult.Error);

                var lamps = lampsResult.Value;

                lamps.Add(lamp);
                Save(lamps);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.NotFound("[LAMP REPOSITORY]", ex.Message));
            }
        }

        public Result Update(Lamp lamp)
        {
            try
            {
                var lamps = Load();

                var index = lamps.FindIndex(l => l.Id == lamp.Id);
                if (index == -1)
                    return Result.Failure(Error.NotFound("[LAMP REPOSITORY]", "Lamp not found"));

                lamps[index] = lamp;
                Save(lamps);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.NotFound("[LAMP REPOSITORY]", ex.Message));
            }
        }

        public Result Remove(Guid id)
        {
            try
            {
                var lamps = Load();

                var lamp = lamps.FirstOrDefault(l => l.Id == id);

                if (lamp is null)
                    return Result.Failure(Error.NotFound("[LAMP REPOSITORY]", "Lamp not found"));

                lamps.Remove(lamp);
                Save(lamps);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.NotFound("[LAMP REPOSITORY]", ex.Message));
            }
        }

        // ---------------- PRIVATE ----------------

        private List<Lamp> Load()
        {
            var json = File.ReadAllText(_filePath);

            var dtos = JsonSerializer.Deserialize<List<LampDto>>(json)
                       ?? new List<LampDto>();

            return dtos.Select(LampMapper.ToDomain).ToList();
        }

        private void Save(List<Lamp> lamps)
        {
            var dtos = lamps.Select(LampMapper.ToDto).ToList();

            var json = JsonSerializer.Serialize(dtos,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_filePath, json);
        }
    }
}
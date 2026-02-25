using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using System.Text.Json;

namespace BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Json.Devices.Lightning
{
    public class JsonLampRepository : ILampRepository
    {
        private readonly string _filePath = "lamps.json";

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

        public List<Lamp> GetAll()
        {
            return Load();
        }

        public Lamp GetById(Guid id)
        {
            return Load().First(l => l.Id == id);
        }

        public void Add(Lamp lamp)
        {
            var lamps = Load();
            lamps.Add(lamp);
            Save(lamps);
        }

        public void Update(Lamp lamp)
        {
            var lamps = Load();

            var index = lamps.FindIndex(l => l.Id == lamp.Id);
            if (index == -1)
                throw new Exception("Lamp not found");

            lamps[index] = lamp;
            Save(lamps);
        }

        public void Remove(Guid id)
        {
            var lamps = Load();
            var lamp = lamps.First(l => l.Id == id);
            lamps.Remove(lamp);
            Save(lamps);
        }

        private List<Lamp> Load()
        {
            var json = File.ReadAllText(_filePath);

            var dtos = JsonSerializer.Deserialize<List<LampDto>>(json) ?? new List<LampDto>();

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

//using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
//using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
//using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
//using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
//using System.Globalization;
//using System.Text.Json;

//namespace BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Csv.Devices.Lightning
//{
//    public class CsvLampRepository : ILampRepository
//    {
//        private readonly string _filePath = "lamps.csv";

//        public CsvLampRepository()
//        {
//            var solutionRoot = LocalPathHelper.GetSolutionRoot();

//            var dataFolder = Path.Combine(solutionRoot, "data");
//            Directory.CreateDirectory(dataFolder);

//            _filePath = Path.Combine(dataFolder, "lamps.csv");

//            if (!File.Exists(_filePath))
//            {
//                Save(new List<Lamp>());
//            }
//        }

//        public List<Lamp> GetAll()
//        {
//            return Load();
//        }

//        public Lamp GetById(Guid id)
//        {
//            return Load().First(l => l.Id == id);
//        }

//        public void Add(Lamp lamp)
//        {
//            var lamps = Load();
//            lamps.Add(lamp);
//            Save(lamps);
//        }

//        public void Update(Lamp lamp)
//        {
//            var lamps = Load();

//            var index = lamps.FindIndex(l => l.Id == lamp.Id);
//            if (index == -1)
//                throw new Exception("Lamp not found");

//            lamps[index] = lamp;
//            Save(lamps);
//        }

//        public void Remove(Guid id)
//        {
//            var lamps = Load();
//            var lamp = lamps.First(l => l.Id == id);
//            lamps.Remove(lamp);
//            Save(lamps);
//        }

//        private List<Lamp> Load()
//        {
//            var lines = File.ReadAllLines(_filePath);

//            if (lines.Length <= 1)
//                return new List<Lamp>();

//            var lamps = new List<Lamp>();

//            foreach (var line in lines.Skip(1)) // salta header
//            {
//                var values = line.Split(',');

//                var dto = new LampDto
//                {
//                    Id = Guid.Parse(values[0]),
//                    Name = values[1],
//                    ImageUrl = values[2],
//                    Status = values[3],
//                    Brightness = int.Parse(values[4]),
//                    CreatedAtUtc = DateTime.Parse(values[5], null, DateTimeStyles.RoundtripKind),
//                    LastModifiedAtUtc = DateTime.Parse(values[6], null, DateTimeStyles.RoundtripKind)
//                };

//                lamps.Add(LampMapper.ToDomain(dto));
//            }

//            return lamps;
//        }

//        private void Save(List<Lamp> lamps)
//        {
//            var dtos = lamps.Select(LampMapper.ToDto).ToList();

//            var lines = new List<string>
//            {
//                "Id,Name,ImageUrl,Status,Brightness,CreatedAtUtc,LastModifiedAtUtc"
//            };

//            foreach (var dto in dtos)
//            {
//                lines.Add(string.Join(",",
//                    dto.Id,
//                    Escape(dto.Name),
//                    Escape(dto.ImageUrl),
//                    dto.Status,
//                    dto.Brightness,
//                    dto.CreatedAtUtc.ToString("O"),
//                    dto.LastModifiedAtUtc.ToString("O")
//                ));
//            }

//            File.WriteAllLines(_filePath, lines);
//        }
//        private string Escape(string value)
//        {
//            if (string.IsNullOrEmpty(value))
//                return "";

//            if (value.Contains(",") || value.Contains("\""))
//            {
//                return $"\"{value.Replace("\"", "\"\"")}\"";
//            }

//            return value;
//        }
//    }
//}

//using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
//using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Mappers;
//using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
//using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
//using System.Xml.Serialization;

//namespace BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps
//{
//    public class XmlLampRepository : ILampRepository
//    {
//        private readonly string _filePath;

//        public XmlLampRepository()
//        {
//            var solutionRoot = LocalPathHelper.GetSolutionRoot();

//            var dataFolder = Path.Combine(solutionRoot, "data");
//            Directory.CreateDirectory(dataFolder);

//            _filePath = Path.Combine(dataFolder, "lamps.xml");

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
//            if (!File.Exists(_filePath))
//                return new List<Lamp>();

//            var serializer = new XmlSerializer(typeof(List<LampDto>));

//            using var stream = new FileStream(_filePath, FileMode.Open);

//            if (stream.Length == 0)
//                return new List<Lamp>();

//            var dtos = (List<LampDto>)serializer.Deserialize(stream)!;

//            return dtos.Select(LampMapper.ToDomain).ToList();
//        }

//        private void Save(List<Lamp> lamps)
//        {
//            var dtos = lamps.Select(LampMapper.ToDto).ToList();

//            var serializer = new XmlSerializer(typeof(List<LampDto>));

//            using var stream = new FileStream(_filePath, FileMode.Create);

//            serializer.Serialize(stream, dtos);
//        }
//    }
//}

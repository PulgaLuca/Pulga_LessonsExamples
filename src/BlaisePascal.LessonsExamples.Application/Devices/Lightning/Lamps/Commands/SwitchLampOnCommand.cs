//using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;
//using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

//namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands
//{
//    public class SwitchOnLampCommand
//    {
//        private readonly ILampRepository _lampRepository;
//        public SwitchOnLampCommand(ILampRepository repository)
//        {
//            _lampRepository = repository;
//        }

//        public void Execute(Guid lampId)
//        {
//            Lamp lamp = _lampRepository.GetById(lampId);
//            if (lamp != null)
//            {
//                lamp.SwitchOn();
//                _lampRepository.Update(lamp);
//            }
//        }
//    }
//}

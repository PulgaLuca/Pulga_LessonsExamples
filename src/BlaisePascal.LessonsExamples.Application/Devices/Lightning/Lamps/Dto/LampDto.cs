using BlaisePascal.LessonsExamples.Domain.Devices.Lightning;

namespace BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto
{
    // public sealed record LampDto(Guid Id, string Name, string ImageUrl, string Status, DateTime CreatedAtUtc, DateTime LastModifiedAtUtc);
    public class LampDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public int Brightness { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastModifiedAtUtc { get; set; }

        public override string ToString()
        {
            return
                $"Id: {Id}\n" +
                $"Name: {Name}\n" +
                $"Status: {Status}\n" +
                $"Brightness: {Brightness}\n" +
                $"Created: {CreatedAtUtc}\n" +
                $"Last update: {LastModifiedAtUtc}\n";
        }
    }

}

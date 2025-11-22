using System;
using System.Collections.Generic;

namespace BlaisePascal.LessonsExamples.Domain.LuminousDevices
{
    public class LampsRowLinq
    {
        private readonly List<AbstractLamp> _lamps;

        private const int DefaultStepAmount = 10;

        public string Name { get; private set; }

        // Computer Properties
        public DeviceStatus Status =>
            _lamps.Any(l => l.Status == DeviceStatus.On)
            ? DeviceStatus.On : DeviceStatus.Off;

        public int AverageIntensity =>
            _lamps.Count == 0 ? 0 : (int)_lamps.Average(l => l.Intensity);

        public int MaxIntensity =>
            _lamps.Count == 0 ? 0 : _lamps.Max(l => l.Intensity);

        public int MinIntensity =>
            _lamps.Count == 0 ? 0 : _lamps.Min(l => l.Intensity);

        // Constructor
        public LampsRowLinq(string name, List<AbstractLamp> lamps)
        {
            Name = name;
            _lamps = new List<AbstractLamp>(lamps);
        }

        // Group operations
        public void SwitchOn() 
            => _lamps.ForEach(l => l.SwitchOn());
        
        public void SwitchOff() 
            => _lamps.ForEach(l => l.SwitchOff());

        public void SetIntensity(int intensity) 
            => _lamps.ForEach(l => l.SetIntensity(intensity));

        public void BrightenAll(int amount = DefaultStepAmount) 
            => _lamps.ForEach(l => l.Brighten(amount));

        public void DimmerAll(int amount = DefaultStepAmount)
            => _lamps.ForEach(l => l.Dimmer(amount));

        // Single lamp operations
        public void SwitchOn(int index) => GetLamp(index).SwitchOn();
        public void SwitchOn(Guid id) => GetLamp(id).SwitchOn();

        public void SwitchOff(int index) => GetLamp(index).SwitchOff();
        public void SwitchOff(Guid id) => GetLamp(id).SwitchOff();

        public void SetIntensity(int index, int value) => GetLamp(index).SetIntensity(value);
        public void SetIntensity(Guid id, int value) => GetLamp(id).SetIntensity(value);

        public void Brighten(int index, int amount) => GetLamp(index).Brighten(amount);
        public void Brighten(Guid id, int amount) => GetLamp(id).Brighten(amount);

        public void Dimmer(int index, int amount) => GetLamp(index).Dimmer(amount);
        public void Dimmer(Guid id, int amount) => GetLamp(id).Dimmer(amount);

        // --- Manage Lamps ---
        public void AddLamp(AbstractLamp lamp)
        {
            if (lamp is null)
                throw new ArgumentNullException(nameof(lamp));

            if (_lamps.Any(l => l.Id == lamp.Id))
                throw new InvalidOperationException($"Lampada con Id {lamp.Id} già presente.");

            _lamps.Add(lamp);
        }

        public void RemoveLamp(AbstractLamp lamp)
        {
            if (lamp is null)
                throw new ArgumentNullException(nameof(lamp));

            if (!_lamps.Remove(lamp))
                throw new InvalidOperationException("La lampada non è presente nella riga.");
        }

        public bool RemoveLamp(Guid id)
        {
            var lamp = FindLampById(id);
            if (lamp == null) return false;
            _lamps.Remove(lamp);
            return true;
        }

        // ANALYTICS / ALGORITMI
        // ------------------------

        public AbstractLamp? FindLampWithMaxIntensity() =>
            _lamps.OrderByDescending(l => l.Intensity).FirstOrDefault();

        public AbstractLamp? FindLampWithMinIntensity() =>
            _lamps.OrderBy(l => l.Intensity).FirstOrDefault();

        public List<AbstractLamp> FindLampsByIntensityRange(int min, int max) =>
            _lamps.Where(l => l.Intensity >= min && l.Intensity <= max).ToList();

        public List<AbstractLamp> FindAllOn() =>
            _lamps.Where(l => l.Status == DeviceStatus.On).ToList();

        public List<AbstractLamp> FindAllOff() =>
            _lamps.Where(l => l.Status == DeviceStatus.Off).ToList();

        public AbstractLamp? FindLampByIntensity(int intensity) =>
            _lamps.FirstOrDefault(l => l.Intensity == intensity);

        public AbstractLamp? FindLampById(Guid id) =>
            _lamps.FirstOrDefault(l => l.Id == id);

        // Sorting con List.Sort (molto più veloce di bubble sort)
        public List<AbstractLamp> SortByIntensity(bool descending = false)
        {
            var sorted = new List<AbstractLamp>(_lamps);
            sorted.Sort((a, b) =>
            {
                int cmp = a.Intensity.CompareTo(b.Intensity);
                return descending ? -cmp : cmp;
            });
            return sorted;
        }

        // Ordina mantenendo la stabilità (Linq OrderBy è stabile)
        public List<AbstractLamp> StableSortByIntensity(bool descending = false) =>
            descending
                ? _lamps.OrderByDescending(l => l.Intensity).ToList()
                : _lamps.OrderBy(l => l.Intensity).ToList();

        // AUTO OFF MANAGEMENT
        public void CheckAutoOff()
        {
            foreach (var lamp in _lamps)
            {
                if (lamp is EcoLamp eco)
                    eco.CheckAutoOff();
            }
        }

        // --- Private Helpers ---
        private AbstractLamp GetLamp(int index)
        {
            if (index < 1 || index > _lamps.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Indice lampada non valido (1-based).");

            return _lamps[index - 1];
        }

        private AbstractLamp GetLamp(Guid id)
        {
            var lamp = FindLampById(id);
            if (lamp == null)
                throw new ArgumentException($"Nessuna lampada trovata con Id {id}.", nameof(id));

            return lamp;
        }
    }
}

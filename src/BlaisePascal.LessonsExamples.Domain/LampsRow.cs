using System;
using System.Collections.Generic;

namespace BlaisePascal.LessonsExamples.Domain
{
    public class LampsRow
    {
        private readonly List<AbstractLamp> _lamps = new();

        public IReadOnlyCollection<AbstractLamp> Lamps => _lamps.AsReadOnly();

        public string Name { get; }

        public DeviceStatus Status
        {
            get
            {
                foreach (var lamp in _lamps)
                {
                    if (lamp.Status == DeviceStatus.On)
                        return DeviceStatus.On;
                }
                return DeviceStatus.Off;
            }
        }

        public int AverageIntensity
        {
            get
            {
                if (_lamps.Count == 0)
                    return 0;

                int sum = 0;
                foreach (var lamp in _lamps)
                    sum += lamp.Intensity;

                return sum / _lamps.Count;
            }
        }

        // --- Costruttori ---
        public LampsRow(string name, List<AbstractLamp> lamps)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Il nome della riga di lampade non può essere vuoto.", nameof(name));

            if (lamps == null)
                throw new ArgumentNullException(nameof(lamps));

            foreach (var lamp in lamps)
            {
                if (lamp == null)
                    throw new ArgumentException("Nessuna lampada può essere null.", nameof(lamps));

                _lamps.Add(lamp);
            }

            if (_lamps.Count == 0)
                throw new ArgumentException("È necessario fornire almeno una lampada.", nameof(lamps));

            Name = name;
        }

        // --- Comandi principali ---
        public void SwitchOn()
        {
            foreach (var lamp in _lamps)
                lamp.SwitchOn();
        }

        public void SwitchOn(int lampIndex)
        {
            GetLamp(lampIndex).SwitchOn();
        }

        public void SwitchOff()
        {
            foreach (var lamp in _lamps)
                lamp.SwitchOff();
        }

        public void SwitchOff(int lampIndex)
        {
            GetLamp(lampIndex).SwitchOff();
        }

        public void SetIntensityAll(int value)
        {
            foreach (var lamp in _lamps)
                lamp.SetIntensity(value);
        }

        public void SetIntensity(int lampIndex, int value)
        {
            GetLamp(lampIndex).SetIntensity(value);
        }

        // --- Utility ---
        public AbstractLamp? FindLampWithMaxIntensity()
        {
            if (_lamps.Count == 0)
                return null;

            AbstractLamp maxLamp = _lamps[0];
            for (int i = 1; i < _lamps.Count; i++)
            {
                if (_lamps[i].Intensity > maxLamp.Intensity)
                    maxLamp = _lamps[i];
            }
            return maxLamp;
        }

        public List<AbstractLamp> SortByIntensity(bool descending = false)
        {
            var sorted = new List<AbstractLamp>(_lamps);

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    bool swap = descending
                        ? sorted[i].Intensity < sorted[j].Intensity
                        : sorted[i].Intensity > sorted[j].Intensity;

                    if (swap)
                    {
                        var temp = sorted[i];
                        sorted[i] = sorted[j];
                        sorted[j] = temp;
                    }
                }
            }

            return sorted;
        }

        public AbstractLamp? FindLampByIntensity(int target)
        {
            foreach (var lamp in _lamps)
            {
                if (lamp.Intensity == target)
                    return lamp;
            }
            return null;
        }

        // --- Gestione lampade ---
        public void AddLamp(AbstractLamp lamp)
        {
            if (lamp == null)
                throw new ArgumentNullException(nameof(lamp));

            if (_lamps.Contains(lamp))
                throw new InvalidOperationException("La lampada è già presente nella riga.");

            _lamps.Add(lamp);
        }

        public void RemoveLamp(AbstractLamp lamp)
        {
            if (!_lamps.Contains(lamp))
                throw new InvalidOperationException("La lampada non esiste nella riga.");

            _lamps.Remove(lamp);
        }

        // --- Helper interno ---
        private AbstractLamp GetLamp(int index)
        {
            if (index < 1 || index > _lamps.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Indice lampada non valido (1-based).");

            return _lamps[index - 1];
        }
    }
}

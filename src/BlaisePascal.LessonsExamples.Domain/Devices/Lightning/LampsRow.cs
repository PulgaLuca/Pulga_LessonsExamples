using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using System;
using System.Collections.Generic;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public class LampsRow
    {
        private readonly List<AbstractLamp> _lamps;

        private const int DefaultDimmerAmount = 10;
        private const int DefaultBrightenAmount = 10;

        public string Name { get; private set; }

        // Computed Properties
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

        public int AverageBrightness
        {
            get
            {
                if (_lamps.Count == 0)
                    return 0;
                int totalBrightness = 0;
                foreach (var lamp in _lamps)
                {
                    totalBrightness += lamp.Brightness.Value;
                }
                return totalBrightness / _lamps.Count;
            }
        }

        public int MaxBrightness
        {
            get
            {
                if (_lamps.Count == 0)
                    return 0;
                int maxBrightness = int.MinValue;
                foreach (var lamp in _lamps)
                {
                    if (lamp.Brightness.Value > maxBrightness)
                        maxBrightness = lamp.Brightness.Value;
                }
                return maxBrightness;
            }
        }

        public int MinBrightness
        {
            get
            {
                if (_lamps.Count == 0)
                    return 0;
                int minBrightness = int.MaxValue;
                foreach (var lamp in _lamps)
                {
                    if (lamp.Brightness.Value < minBrightness)
                        minBrightness = lamp.Brightness.Value;
                }
                return minBrightness;
            }
        }

        // Constructor
        public LampsRow(string name, List<AbstractLamp> lamps)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome non valido.", nameof(name));

            if (lamps == null)
                throw new ArgumentNullException(nameof(lamps));

            if (lamps.Count == 0)
                throw new ArgumentException("Fornire almeno una lampada.", nameof(lamps));

            _lamps = new List<AbstractLamp>();

            for (int i = 0; i < lamps.Count; i++)
            {
                if (lamps[i] == null)
                    throw new ArgumentException("Una lampada è null.", nameof(lamps));

                _lamps.Add(lamps[i]);
            }

            Name = name;
        }

        // Group operations
        public void SwitchOn()
        {
            for (int i = 0; i < _lamps.Count; i++)
                _lamps[i].SwitchOn();
        }

        public void SwitchOff()
        {
            for (int i = 0; i < _lamps.Count; i++)
                _lamps[i].SwitchOff();
        }

        //public void SetBrightnessAll(int value)
        //{
        //    for (int i = 0; i < _lamps.Count; i++)
        //        _lamps[i].SetBrightness(Brightness.From(value));
        //}

        public void BrightenAll(int amount = DefaultBrightenAmount)
        {
            for (int i = 0; i < _lamps.Count; i++)
                _lamps[i].Brighten(amount);
        }

        public void DimmerAll(int amount = DefaultDimmerAmount)
        {
            for (int i = 0; i < _lamps.Count; i++)
                _lamps[i].Dimmer(amount);
        }

        // Single lamp operations
        public void SwitchOn(int index) => GetLamp(index).SwitchOn();
        public void SwitchOn(Guid id) => GetLamp(id).SwitchOn();

        public void SwitchOff(int index) => GetLamp(index).SwitchOff();
        public void SwitchOff(Guid id) => GetLamp(id).SwitchOff();

        //public void SetBrightness(int index, int value) => GetLamp(index).SetBrightness(Brightness.From(value));
        //public void SetBrightness(Guid id, int value) => GetLamp(id).SetBrightness(Brightness.From(value));

        public void Brighten(int index, int amount) => GetLamp(index).Brighten(amount);
        public void Brighten(Guid id, int amount) => GetLamp(id).Brighten(amount);

        public void Dimmer(int index, int amount) => GetLamp(index).Dimmer(amount);
        public void Dimmer(Guid id, int amount) => GetLamp(id).Dimmer(amount);


        // --- ANALYTICS E RICERCHE ---
        public AbstractLamp? FindLampWithMaxBrightness()
        {
            if (_lamps.Count == 0)
                return null;

            AbstractLamp maxLamp = _lamps[0];

            for (int i = 1; i < _lamps.Count; i++)
            {
                if (_lamps[i].Brightness.Value > maxLamp.Brightness.Value)
                    maxLamp = _lamps[i];
            }

            return maxLamp;
        }

        public AbstractLamp? FindLampWithMinBrightness()
        {
            if (_lamps.Count == 0)
                return null;

            AbstractLamp minLamp = _lamps[0];

            for (int i = 1; i < _lamps.Count; i++)
            {
                if (_lamps[i].Brightness.Value < minLamp.Brightness.Value)
                    minLamp = _lamps[i];
            }

            return minLamp;
        }

        public List<AbstractLamp> FindLampsByBrightnessRange(int min, int max)
        {
            var result = new List<AbstractLamp>();

            for (int i = 0; i < _lamps.Count; i++)
            {
                int Brightness = _lamps[i].Brightness.Value;

                if (Brightness >= min && Brightness <= max)
                    result.Add(_lamps[i]);
            }

            return result;
        }

        public List<AbstractLamp> FindAllOn()
        {
            var result = new List<AbstractLamp>();

            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Status == DeviceStatus.On)
                    result.Add(_lamps[i]);
            }

            return result;
        }

        public List<AbstractLamp> FindAllOff()
        {
            var result = new List<AbstractLamp>();

            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Status == DeviceStatus.Off)
                    result.Add(_lamps[i]);
            }

            return result;
        }

        public AbstractLamp? FindLampByBrightness(int value)
        {
            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Brightness.Value == value)
                    return _lamps[i];
            }

            return null;
        }

        public AbstractLamp? FindLampById(Guid id)
        {
            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Id == id)
                    return _lamps[i];
            }

            return null;
        }

        // SORTING (implementato esplicitamente)
        // ------------------------

        public List<AbstractLamp> SortByBrightness(bool descending)
        {
            var result = CopyLamps();
            SortByBrightnessInternal(result, ascending: !descending);
            return result;
        }

        private void SortByBrightnessInternal(List<AbstractLamp> list, bool ascending)
        {
            // Selection sort (O(n²), stabile se non scambiamo oggetti uguali)
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int bestIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    bool condition = ascending
                        ? list[j].Brightness.Value < list[bestIndex].Brightness.Value
                        : list[j].Brightness.Value > list[bestIndex].Brightness.Value;

                    if (condition)
                        bestIndex = j;
                }

                if (bestIndex != i)
                {
                    var temp = list[i];
                    list[i] = list[bestIndex];
                    list[bestIndex] = temp;
                }
            }
        }

        // ------------------------
        // AUTO OFF
        // ------------------------

        public void CheckAutoOff()
        {
            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i] is EcoLamp eco)
                    eco.CheckAutoOff();
            }
        }

        // GESTIONE LAMPADE
        // ------------------------

        public void AddLamp(AbstractLamp lamp)
        {
            if (lamp == null)
                throw new ArgumentNullException(nameof(lamp));

            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Id == lamp.Id)
                    throw new InvalidOperationException($"Lampada con Id {lamp.Id} già presente.");
            }

            _lamps.Add(lamp);
        }

        public void RemoveLamp(AbstractLamp lamp)
        {
            if (lamp == null)
                throw new ArgumentNullException(nameof(lamp));

            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i] == lamp)
                {
                    _lamps.RemoveAt(i);
                    return;
                }
            }

            throw new InvalidOperationException("Lampada non trovata.");
        }

        public bool RemoveLamp(Guid id)
        {
            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Id == id)
                {
                    _lamps.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        // PRIVATE UTILITIES
        private AbstractLamp GetLamp(int index)
        {
            if (index < 1 || index > _lamps.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Indice 1-based non valido.");

            return _lamps[index - 1];
        }

        private AbstractLamp GetLamp(Guid id)
        {
            for (int i = 0; i < _lamps.Count; i++)
            {
                if (_lamps[i].Id == id)
                    return _lamps[i];
            }

            throw new ArgumentException($"Nessuna lampada trovata con Id {id}.", nameof(id));
        }

        private List<AbstractLamp> CopyLamps()
        {
            var list = new List<AbstractLamp>(_lamps.Count);
            for (int i = 0; i < _lamps.Count; i++)
                list.Add(_lamps[i]);
            return list;
        }
    }
}

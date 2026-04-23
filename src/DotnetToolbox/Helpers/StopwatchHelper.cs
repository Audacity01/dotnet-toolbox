using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotnetToolbox.Helpers
{
    public class StopwatchHelper
    {
        private readonly Stopwatch _stopwatch = new();
        private readonly List<(string label, TimeSpan elapsed)> _laps = new();

        public void Start() => _stopwatch.Start();
        public void Stop() => _stopwatch.Stop();
        public void Reset() { _stopwatch.Reset(); _laps.Clear(); }

        public TimeSpan Elapsed => _stopwatch.Elapsed;
        public long ElapsedMs => _stopwatch.ElapsedMilliseconds;

        public void Lap(string label = null)
        {
            label ??= $"Lap {_laps.Count + 1}";
            _laps.Add((label, _stopwatch.Elapsed));
        }

        public T Measure<T>(Func<T> action, string label = null)
        {
            var sw = Stopwatch.StartNew();
            var result = action();
            sw.Stop();
            label ??= $"Operation {_laps.Count + 1}";
            _laps.Add((label, sw.Elapsed));
            return result;
        }

        public void Measure(Action action, string label = null)
        {
            Measure(() => { action(); return 0; }, label);
        }

        public string GetReport()
        {
            var lines = new List<string> { $"Total: {_stopwatch.Elapsed.TotalMilliseconds:F2}ms" };
            TimeSpan prev = TimeSpan.Zero;
            foreach (var (label, elapsed) in _laps)
            {
                var diff = elapsed - prev;
                lines.Add($"  {label}: {diff.TotalMilliseconds:F2}ms (at {elapsed.TotalMilliseconds:F2}ms)");
                prev = elapsed;
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}

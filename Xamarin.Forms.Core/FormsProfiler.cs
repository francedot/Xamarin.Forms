using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xamarin.Forms
{
    public static class FormsProfiler
    {
		static readonly Dictionary<string, Stats> Statistics = new Dictionary<string, Stats>();

        public static void DumpStats()
        {
            Debug.WriteLine(GetStats());
        }

        public static string GetStats()
        {
            var b = new StringBuilder();
            b.AppendLine();
            b.Append($"{"ID",-40} | {"Call Count",-10} | {"Total Time",-12} | {"Avg Time",-10}");
            b.AppendLine();
            foreach (KeyValuePair<string, Stats> kvp in Statistics.OrderBy(kvp => kvp.Key))
            {
                string key = kvp.Key;
                double total = TimeSpan.FromTicks(kvp.Value.TotalTime).TotalMilliseconds;
                double avg = total / kvp.Value.CallCount;
                b.Append($"{key,-40} | {kvp.Value.CallCount,-10} | {total,-10:N4}ms | {avg,-8:N4}ms");
                b.AppendLine();
            }
            return b.ToString();
        }

        public static void Start(string tag = null, [CallerMemberName] string member = null)
        {
            string id = member + (tag != null ? "-" + tag : string.Empty);

            Stats stats;
            if (!Statistics.TryGetValue(id, out stats))
                Statistics[id] = stats = new Stats();

            stats.CallCount++;
            stats.StartTimes.Push(Stopwatch.GetTimestamp());
        }

        public static void Stop(string tag = null, [CallerMemberName] string member = null)
        {
            string id = member + (tag != null ? "-" + tag : string.Empty);
            long stop = Stopwatch.GetTimestamp();

            Stats stats = Statistics[id];
            long start = stats.StartTimes.Pop();
            if (!stats.StartTimes.Any())
                stats.TotalTime += stop - start;
        }

        class Stats
        {
            public readonly Stack<long> StartTimes = new Stack<long>();
            public int CallCount;
            public long TotalTime;
        }
    }
}
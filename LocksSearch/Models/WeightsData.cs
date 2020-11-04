using System;
using System.Collections.Generic;

namespace LocksSearch.Models
{
    internal static class WeightsData
    {
        public static Dictionary<string, int> Building = new Dictionary<string, int>
        {
            {"shortCut", 7},
            {"name", 9},
            {"decsription", 5}
        };

        public static Dictionary<string, int> Lock = new Dictionary<string, int>
        {
            {"type", 3 },
            {"name", 10},
            {"serialNumber", 8},
            {"floor", 6},
            {"roomNumber", 6},
            {"decsription", 6}
        };

        public static Dictionary<string, int> Group = new Dictionary<string, int>
        {
            {"name", 9},
            {"decsription", 5}
        };

        public static Dictionary<string, int> Media = new Dictionary<string, int>
        {
            {"type", 3},
            {"owner", 10},
            {"serialNumber", 8},
            {"decsription", 6}
        };
    }
}

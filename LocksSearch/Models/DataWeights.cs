using System;
using System.Collections.Generic;

namespace LocksSearch.Models
{
    internal static class WeightsData
    {
        public static Dictionary<string, int> Building = new Dictionary<string, int>
        {
            {"ShortCut", 7},
            {"Name", 9},
            {"Decsription", 5}
        };

        public static Dictionary<string, int> Lock = new Dictionary<string, int>
        {
            {"Type", 3 },
            {"Name", 10},
            {"SerialNumber", 8},
            {"Floor", 6},
            {"RoomNumber", 6},
            {"Description", 6},
            // transitive property
            {"Building:Name", 8 },
            // transitive property
            {"Building:ShortCut", 8 },
            // transitive property
            {"Building:Description", 0 }
        };

        public static Dictionary<string, int> Group = new Dictionary<string, int>
        {
            {"Name", 9},
            {"Description", 5}
        };

        public static Dictionary<string, int> Media = new Dictionary<string, int>
        {
            {"Type", 3},
            {"Owner", 10},
            {"SerialNumber", 8},
            {"Description", 6},
            // transitive property
            {"Group:Name", 8 },
            // transitive property
            {"Group:Description", 0 }
        };
    }
}

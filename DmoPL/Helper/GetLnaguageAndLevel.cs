using System.Collections.Generic;

namespace DmoPL.Helper
{
    public static class GetLnaguageAndLevel
    {
        public static List<string> Language { get; set; } = new List<string>() { "Arabic", "English", "Franch", "Italian", "German" };
        public static List<string> LanguageLevel { get; set; } = new List<string>() { "A1", "A2", "B1", "B2", "C1", "C2" };
    }
}

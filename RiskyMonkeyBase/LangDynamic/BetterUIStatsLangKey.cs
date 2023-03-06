using BetterUI;
using RoR2;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RiskyMonkeyBase.LangDynamic
{
    public class BetterUIStatsLangKey
    {

        public class LangKeyReplaceEntry
        {
            public int start;
            public int end;
            public string key;
            public LangKeyReplaceEntry(int start, int end, string str)
            {
                this.start = start;
                this.end = end;
                key = str.Substring(start + 2, end - start - 2);
            }

            public string replace(string str)
            {
                return str.Substring(0, start) + Language.GetString(key) + str.Substring(end);
            }
        }

        public static List<LangKeyReplaceEntry> normalList;
        public static List<LangKeyReplaceEntry> altList;
        public static string normalText;
        public static string altText;

        public static void Patch()
        {
            normalList = new List<LangKeyReplaceEntry>();
            altList = new List<LangKeyReplaceEntry>();
            normalText = ConfigManager.StatsDisplayStatString.Value;
            altText = ConfigManager.StatsDisplayStatStringCustomBind.Value;
            int index = 0;
            while (true)
            {
                index = normalText.IndexOf("$$", index);
                if (index == -1) break;
                int endIndex = index + 2;
                while (endIndex < normalText.Length)
                {
                    if (!char.IsLetterOrDigit(normalText[endIndex]) && normalText[endIndex] != '_') break;
                    endIndex++;
                }
                RiskyMonkeyBase.Log.LogInfo("replacing " + normalText.Substring(index, endIndex - index) + " for normalText");
                normalList.Add(new LangKeyReplaceEntry(index, endIndex, normalText));
                index = endIndex;
            }
            index = 0;
            while (true)
            {
                index = altText.IndexOf("$$", index);
                if (index == -1) break;
                int endIndex = index + 2;
                while (endIndex < altText.Length)
                {
                    if (!char.IsLetterOrDigit(altText[endIndex]) && altText[endIndex] != '_') break;
                    endIndex++;
                }
                RiskyMonkeyBase.Log.LogInfo("replacing " + altText.Substring(index, endIndex - index) + " for altText");
                altList.Add(new LangKeyReplaceEntry(index, endIndex, altText));
                index = endIndex;
            }
            Language.onCurrentLanguageChanged += PostPatch;
        }

        public static void PostPatch()
        {
            normalList.Reverse();
            altList.Reverse();
            foreach (var entry in normalList) normalText = entry.replace(normalText);
            foreach (var entry in altList) altText = entry.replace(altText);
            StatsDisplay.normalText = StatsDisplay.regexpattern.Split(normalText);
            StatsDisplay.altText = StatsDisplay.regexpattern.Split(altText);
        }
    }
}

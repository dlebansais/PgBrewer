namespace PgBrewer;

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WpfLayout;

/// <summary>
/// Reads and write settings.
/// </summary>
public partial class DataArchive
{
    private static readonly DataArchive Default = new();

    public static async Task<List<Effect>> ReadEffectList(string name)
    {
        byte[] EffectListBytes = await SystemTools.GetResourceFile("EffectList.txt");
        using MemoryStream EffectListStream = new MemoryStream(EffectListBytes);
        return ReadEffectList(EffectListStream, name);
    }

    private static List<Effect> ReadEffectList(Stream resourceStream, string name)
    {
        List<Effect> Result = new List<Effect>();

        using StreamReader Reader = new StreamReader(resourceStream, Encoding.UTF8);

        for (; ;)
        {
            string? Line = Reader.ReadLine()!;
            if (Line == null)
                break;

            string[] LineSplit = Line.Split(';');
            if (LineSplit.Length > 1)
            {
                string Name = LineSplit[0];
                string Text = LineSplit[1];
                string? Prefix = LineSplit.Length > 3 ? LineSplit[2] : null;
                string? Suffix = LineSplit.Length > 3 ? LineSplit[3] : null;

                if (Name == name)
                    Result.Add(new Effect(Text, Prefix, Suffix));
            }
        }

        return Result;
    }

    public static async Task<Settings> ReadSettings()
    {
        if (await MainWindowUI.GetItemAsync<Settings>(string.Empty) is Settings Settings)
            return Settings;
        else
            return new Settings();
    }

    public static List<int> GetIndexList(Settings settings, string valueName, int minLength)
    {
        List<int> Result;
        if (settings.IndexTable.ContainsKey(valueName))
            Result = settings.IndexTable[valueName];
        else
            Result = new();

        for (int i = Result.Count; i < minLength; i++)
            Result.Add(-1);

        return Result;
    }

    public static void SetIndexList(Settings settings, string valueName, List<int> valueList)
    {
        if (settings.IndexTable.ContainsKey(valueName))
            settings.IndexTable[valueName] = valueList;
        else
            settings.IndexTable.Add(valueName, valueList);
    }

    public static async Task WriteSettings(Settings settings)
    {
        await MainWindowUI.SetItemAsync<Settings>(string.Empty, settings);
    }
}

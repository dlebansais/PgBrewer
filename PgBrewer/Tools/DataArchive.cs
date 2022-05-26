namespace PgBrewer;

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using WpfLayout;

public class DataArchive
{
    public static List<Effect> ReadEffectList(string name)
    {
        byte[] EffectListBytes = SystemTools.GetResourceFile("EffectList.txt");
        using MemoryStream EffectListStream = new MemoryStream(EffectListBytes);
        return ReadEffectList(EffectListStream, name);
    }

    public static List<Effect> ReadEffectList(Stream resourceStream, string name)
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

    public static async Task<List<int>> GetIndexList(string valueName, int minLength)
    {
        Settings Settings = await LocalStorage.GetItemAsync<Settings>(valueName);
        List<int> Result = Settings.IndexList;

        for (int i = Result.Count; i < minLength; i++)
            Result.Add(-1);

        return Result;
    }

    public static async Task SetIndexList(string valueName, List<int> valueList)
    {
        Settings Settings = new();
        Settings.IndexList = valueList;

        await LocalStorage.SetItemAsync<Settings>(valueName, Settings);
    }
}

namespace PgBrewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Microsoft.Win32;

    public class DataArchive
    {
        public static List<Effect> ReadEffectList(string name)
        {
            List<Effect> Result = new List<Effect>();

            try
            {
                Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
                string[] ManifestResourceNames = CurrentAssembly.GetManifestResourceNames();
                foreach (string ResourceName in ManifestResourceNames)
                {
                    if (ResourceName.EndsWith(".Resources.EffectList.txt"))
                    {
                        Result = ReadEffectList(CurrentAssembly, ResourceName, name);
                        break;
                    }
                }
            }
            catch
            {
            }

            return Result;
        }

        public static List<Effect> ReadEffectList(Assembly assembly, string resourceName, string name)
        {
            List<Effect> Result = new List<Effect>();

            using (Stream ResourceStream = assembly.GetManifestResourceStream(resourceName)!)
            {
                using (StreamReader Reader = new StreamReader(ResourceStream, Encoding.UTF8))
                {
                    for (; ;)
                    {
                        string Line = Reader.ReadLine()!;
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
                }
            }

            return Result;
        }

        public static RegistryKey OpenSoftwareKey()
        {
            RegistryKey Key = OpenKey(Registry.CurrentUser, "Software", "Project Gorgon Tools", "PgBrewer")!;
            return Key;
        }

        public static List<int> GetIndexList(string valueName, int minLength)
        {
            List<int> Result = new List<int>();

            RegistryKey Key = OpenSoftwareKey();
            string ValueAsString = (string)Key.GetValue(valueName)!;
            Key.Close();

            if (ValueAsString != null)
            {
                string[] Split = ValueAsString.Split(',');
                foreach (string Item in Split)
                {
                    if (int.TryParse(Item, out int Value))
                        Result.Add(Value);
                    else
                        Result.Add(-1);
                }
            }

            for (int i = Result.Count; i < minLength; i++)
                Result.Add(-1);

            return Result;
        }

        public static void SetIndexList(string valueName, List<int> valueList)
        {
            string ValueAsString = string.Empty;

            foreach (int Value in valueList)
            {
                if (ValueAsString.Length > 0)
                    ValueAsString += ",";

                ValueAsString += Value.ToString();
            }

            RegistryKey Key = OpenSoftwareKey();
            Key.SetValue(valueName, ValueAsString);
            Key.Close();
        }

        public static RegistryKey? OpenKey(RegistryKey hive, params string[] path)
        {
            RegistryKey? Key = null;
            string KeyPath = string.Empty;

            for (int i = 0; i < path.Length; i++)
            {
                string Name = path[i];

                if (KeyPath.Length > 0)
                    KeyPath += '\\';

                KeyPath += Name;

                if (Key != null)
                    Key.Close();

                Key = hive.CreateSubKey(KeyPath);
            }

            return Key;
        }
    }
}

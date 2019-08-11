namespace PgBrew
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class DataArchive
    {
        static DataArchive()
        {
            try
            {
                Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
                string ResourcePath = $"PgBrew.Resources.EffectList.txt";

                using (Stream ResourceStream = CurrentAssembly.GetManifestResourceStream(ResourcePath))
                {
                    using (StreamReader Reader = new StreamReader(ResourceStream, Encoding.UTF8))
                    {
                        for (;;)
                        {
                            string Line = Reader.ReadLine();
                            if (Line == null)
                                break;

                            string[] LineSplit = Line.Split(';');
                            if (LineSplit.Length == 2)
                            {
                                string Id = LineSplit[0];
                                string Text = LineSplit[1];

                                switch (Id)
                                {
                                    case "Basic Lager":
                                        BasicLagerTable.Add(Text);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static List<string> BasicLagerTable { get; } = new List<string>();
    }
}

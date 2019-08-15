namespace PgBrew
{
    public class Effect
    {
        public Effect(string text, string prefix, string suffix)
        {
            Text = text;
            Prefix = prefix;
            Suffix = suffix;
        }

        public string Text { get; }
        public string Prefix { get; }
        public string Suffix { get; }
    }
}

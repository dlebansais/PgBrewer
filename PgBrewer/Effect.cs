namespace PgBrewer
{
    public class Effect
    {
        #region Init
        public Effect(string text, string? prefix, string? suffix)
        {
            Text = text;
            Prefix = prefix;
            Suffix = suffix;
        }
        #endregion

        #region Properties
        public string Text { get; }
        public string? Prefix { get; }
        public string? Suffix { get; }
        #endregion
    }
}

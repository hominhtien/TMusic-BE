namespace Tmusic.Localization
{
    public sealed class LocalizableText
    {
        public string Text { get; }

        public IEnumerable<string> Parameters { get; }

        private LocalizableText() { }

        public LocalizableText(string text, IEnumerable<string> parameters)
        {
            Text = text;
            Parameters = parameters;
        }
    }
}

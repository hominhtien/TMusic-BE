using Tmusic.Localization;

namespace Tmusic.Extentions
{
    [Serializable]
    public class CoreException : Exception
    {
        public LocalizableText LocalizableMessage { get; }

        public CoreException() : base() { }

        public CoreException(string message) : base(message)
        {
        }

        public CoreException(LocalizableText message)
        {
            LocalizableMessage = message;
        }

        public static CoreException Exception(string message)
        {
            return new CoreException(message);
        }

        public static CoreException NullArgument(string arg)
        {
            return CoreException.Exception($"{arg} cannot be null");
        }

        public static CoreException InvalidArgument(string arg)
        {
            return CoreException.Exception($"{arg} is invalid");
        }

        public static CoreException NotFound(string arg)
        {
            return CoreException.Exception($"{arg} was not found");
        }
    }
}

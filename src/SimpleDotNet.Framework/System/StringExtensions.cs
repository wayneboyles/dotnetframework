using SimpleDotNet.Framework;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        public static T ToEnum<T>(this string value)
            where T : struct
        {
            Check.NotNull(value, nameof(value));
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            Check.NotNull(value, nameof(value));
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}

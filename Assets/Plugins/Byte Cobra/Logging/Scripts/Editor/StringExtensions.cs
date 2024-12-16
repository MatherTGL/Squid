namespace ByteCobra.Logging.Editor
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}
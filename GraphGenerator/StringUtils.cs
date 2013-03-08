
namespace GraphGenerator
{
    static class StringUtils
    {
        public static string StripDoubleQuotes(this string str)
        {
            int startIndex = 0;
            int lastIndex = str.Length - 1;

            if (str[0] == '"')
            {
                startIndex++;
            }
            if (str[lastIndex] == '"')
            {
                lastIndex--;
            }
            string returnString = str.Substring(startIndex, (lastIndex - startIndex) + 1);
            return returnString;
        }
    }
}

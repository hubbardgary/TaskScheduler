namespace TaskScheduler.Web.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveFromEnd(this string text, string patternToRemove)
        {
            if (text.EndsWith(patternToRemove))
            {
                text = text.Substring(0, text.LastIndexOf(patternToRemove));
            }

            return text;
        }
    }
}
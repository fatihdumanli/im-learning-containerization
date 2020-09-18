namespace Basket.API.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64String(this string phrase)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(phrase);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
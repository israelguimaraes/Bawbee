namespace Bawbee.Infra.CrossCutting.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !IsEmpty(value);
        }
    }
}

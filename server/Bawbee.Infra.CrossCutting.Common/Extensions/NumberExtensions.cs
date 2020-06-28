namespace Bawbee.Infra.CrossCutting.Common.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsGreaterThanZero(this int value)
        {
            return value > 0;
        }
    }
}

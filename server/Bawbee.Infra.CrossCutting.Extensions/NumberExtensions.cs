namespace Bawbee.Infra.CrossCutting.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsGreaterThanZero(this int value)
        {
            return value > 0;
        }

        public static decimal ToNegative(this decimal value)
        {
            if (value > 0)
                return value * -1;

            return value;
        }
    }
}

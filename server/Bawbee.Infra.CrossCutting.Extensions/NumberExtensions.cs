using System;

namespace Bawbee.Infra.CrossCutting.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsNotZero(this decimal value)
        {
            return value != 0;
        }

        public static bool IsGreaterThanZero(this int value)
        {
            return value > 0;
        }

        public static decimal ToNegative(this decimal value)
        {
            return value > 0 ? (value * -1) : value;
        }

        public static decimal ToPositive(this decimal value)
        {
            return value < 0 ? (value * -1) : value;
        }

        public static decimal To2DecimalPlaces(this decimal value)
        {
            return Math.Round(value, 2);
        }
    }
}

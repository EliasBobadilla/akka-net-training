using System;
using Akka.Actor;

namespace Unit1
{
    public static class Utils
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEquals(this string firstValue, string secondValue)
        {
            return string.Equals(firstValue, secondValue, StringComparison.OrdinalIgnoreCase);
        }

        public static void Print(this string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
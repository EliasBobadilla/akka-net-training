using System;
using Akka.Actor;

namespace Unit1
{
    public static class Utils
    {
        public static bool IsExitCommand(this string message, IUntypedActorContext context)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                return false;

            if (!string.Equals(message, "exit", StringComparison.OrdinalIgnoreCase)) return false;
            
            context.System.Terminate();
            return true;
        }
    }
}
using System;
using Akka.Actor;

namespace Unit1
{
    internal static class Program
    {
        private static ActorSystem _system;

        private static void Main(string[] args)
        {
            _system = ActorSystem.Create("actorSystem");
            var writer = _system.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            var reader = _system.ActorOf(Props.Create(() => new ConsoleReaderActor(writer)));
           
            PrintInstructions();
            reader.Tell("start");

            // blocks the main thread from exiting until the actor system is shut down
            _system.WhenTerminated.Wait();
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" red ");
            Console.ResetColor();
            Console.Write(" and others will appear as");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" green! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
}
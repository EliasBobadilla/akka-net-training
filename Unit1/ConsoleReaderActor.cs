using System;
using Akka.Actor;

namespace Unit1
{
    internal class ConsoleReaderActor : UntypedActor
    {

        public const string StartCommand = "start";
        private const string ExitCommand = "exit";


        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }
            GetAndValidateInput("ValidatorActor");
        }


        private static void DoPrintInstructions()
        {
            Console.WriteLine("Please provide the URI of a log file on disk.\n");
        }

        private void GetAndValidateInput(string actor)
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) && string.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }
            var childActor = Context.ActorSelection($"akka://systemActor/user/{actor}");
            childActor.Tell(message);
        }
    }
}
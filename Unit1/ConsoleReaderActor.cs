using System;
using Akka.Actor;

namespace Unit1
{
    internal class ConsoleReaderActor : UntypedActor
    {
        private readonly IActorRef _child;
        public const string StartCommand = "start";
        private const string ExitCommand = "exit";

        public ConsoleReaderActor(IActorRef child)
        {
            _child = child;
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }
            GetAndValidateInput();
        }


        private static void DoPrintInstructions()
        {
            Console.WriteLine("Please provide the URI of a log file on disk.\n");
        }

        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) && string.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }
            _child.Tell(message);
        }
    }
}
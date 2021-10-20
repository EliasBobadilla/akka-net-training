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
            
            GetAndValidateInput();
        }
        
        private static void DoPrintInstructions()
        {
            "Please provide the URI of a log file on disk.\n".Print();
        }

        private static void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!message.IsNullOrEmptyOrWhiteSpace() && message.IsEquals(ExitCommand))
            {
                Context.System.Terminate();
                return;
            }
            var childActor = Context.ActorSelection("akka://systemActor/user/ValidatorActor");
            childActor.Tell(message);
        }
    }
}
using System;
using Akka.Actor;

namespace Unit1
{
    internal class ConsoleReaderActor : UntypedActor
    {
        private readonly IActorRef _child;

        public ConsoleReaderActor(IActorRef child)
        {
            _child = child;
        }

        protected override void OnReceive(object message)
        {
            var read = Console.ReadLine();
            if(read.IsExitCommand(Context)) return;
            
            _child.Tell(read);
            Self.Tell("continue");
        }

    }
}
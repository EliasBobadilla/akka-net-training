using System;
using Akka.Actor;

namespace Unit1
{
    internal static class Program
    {
        private static ActorSystem _system;

        private static void Main(string[] args)
        {
            _system = ActorSystem.Create("systemActor");
            var writer = _system.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            var reader = _system.ActorOf(Props.Create(() => new ConsoleReaderActor(writer)));
            
            var bbbb = _system.ActorOf(Props.Create<ConsoleWriterActor>(), "Hi");

            
            reader.Tell(ConsoleReaderActor.StartCommand);
            _system.WhenTerminated.Wait();
        }
    }
}
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
            var writer = _system.ActorOf(Props.Create(() => new ConsoleWriterActor()), "WriterActor");
            var coordinator = _system.ActorOf(Props.Create(() => new TailCoordinatorActor()),"TailCoordinatorActor");
            var validator = _system.ActorOf(Props.Create(() => new FileValidatorActor(writer)),"ValidatorActor");
            var reader = _system.ActorOf(Props.Create(() => new ConsoleReaderActor()), "ReaderActor");
            
            reader.Tell(ConsoleReaderActor.StartCommand);
            _system.WhenTerminated.Wait();
        }
        // C:\Users\yuri.bobadilla\Downloads\akka_file_demo.txt
    }
}
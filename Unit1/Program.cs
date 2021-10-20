using Akka.Actor;

namespace Unit1
{
    internal static class Program
    {
        private static ActorSystem _system;
        private static IActorRef _writer;
        private static void Main()
        {
            _system = ActorSystem.Create("systemActor");
            _writer = _system.ActorOf(Props.Create(() => new ConsoleWriterActor()), "WriterActor");
            _system.ActorOf(Props.Create(() => new TailCoordinatorActor()),"TailCoordinatorActor");
            _system.ActorOf(Props.Create(() => new FileValidatorActor(_writer)),"ValidatorActor");

            var reader = _system.ActorOf(Props.Create(() => new ConsoleReaderActor()), "ReaderActor");
            reader.Tell(ConsoleReaderActor.StartCommand);
            _system.WhenTerminated.Wait();
        }
    }
}
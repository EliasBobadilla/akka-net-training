using System.IO;
using Akka.Actor;

namespace Unit1
{
    public class FileValidatorActor : UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;

        public FileValidatorActor(IActorRef consoleWriterActor)
             
        {
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            var msg = message as string;
            if (string.IsNullOrEmpty(msg))
            {
                _consoleWriterActor.Tell(new Messages.NullInputError("Input was blank. Please try again.\n"));
                Sender.Tell(new Messages.ContinueProcessing());
            }
            else
            {
                var valid = IsFileUri(msg);
                if (valid)
                {
                    _consoleWriterActor.Tell(new Messages.InputSuccess(
                        $"Starting processing for {msg}"));

                    var coordinator = Context.ActorSelection("akka://systemActor/user/TailCoordinatorActor");
                    coordinator.Tell(new TailCoordinatorActor.StartTail(msg,
                        _consoleWriterActor));
                }
                else
                {
                    _consoleWriterActor.Tell(new Messages.ValidationError(
                        $"{msg} is not an existing URI on disk."));

                    Sender.Tell(new Messages.ContinueProcessing());
                }
            }
        }
        
        private static bool IsFileUri(string path)
        {
            return File.Exists(path);
        }
    }
}
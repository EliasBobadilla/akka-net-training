using System.IO;
using System.Text;
using Akka.Actor;

namespace Unit1
{
    public class TailActor : UntypedActor
    {
        private readonly string _filePath;
        private readonly IActorRef _reporterActor;
        private FileObserver _observer;
        private Stream _fileStream;
        private StreamReader _fileStreamReader;

        public TailActor(IActorRef reporterActor, string filePath)
        {
            _reporterActor = reporterActor;
            _filePath = filePath;
        }
        
        public class FileWrite
        {
            public FileWrite(string fileName)
            {
                FileName = fileName;
            }

            private string FileName { get; set; }
        }

        public class FileError
        {
            public FileError(string fileName, string reason)
            {
                FileName = fileName;
                Reason = reason;
            }

            private string FileName { get; set; }

            public string Reason { get; private set; }
        }

        private class InitialRead
        {
            public InitialRead(string fileName, string text)
            {
                FileName = fileName;
                Text = text;
            }

            private string FileName { get; set; }
            public string Text { get; private set; }
        }

        protected override void PreStart()
        {
            _observer = new FileObserver(Self, Path.GetFullPath(_filePath));
            _observer.Start();

            _fileStream = new FileStream(Path.GetFullPath(_filePath),
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _fileStreamReader = new StreamReader(_fileStream, Encoding.UTF8);

            var text = _fileStreamReader.ReadToEnd();
            Self.Tell(new InitialRead(_filePath, text));
        }


        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case FileWrite:
                {
                    var text = _fileStreamReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                    {
                        _reporterActor.Tell(text);
                    }

                    break;
                }
                case FileError error:
                {
                    _reporterActor.Tell($"Tail error: {error.Reason}");
                    break;
                }
                case InitialRead read:
                {
                    _reporterActor.Tell(read.Text);
                    break;
                }
            }
        }
        
        protected override void PostStop()
        {
            _observer.Dispose();
            _observer = null;
            _fileStreamReader.Close();
            _fileStreamReader.Dispose();
            base.PostStop();
        }
    }
}
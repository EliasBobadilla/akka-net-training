using System;
using Akka.Actor;

namespace Unit1
{
    public class TailCoordinatorActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is not StartTail msg) return;
            Context.ActorOf(Props.Create(
                () => new TailActor(msg.ReporterActor, msg.FilePath)));
        }

        public class StartTail
        {
            public StartTail(string filePath, IActorRef reporterActor)
            {
                FilePath = filePath;
                ReporterActor = reporterActor;
            }

            public string FilePath { get; private set; }

            public IActorRef ReporterActor { get; private set; }
        }

        public class StopTail
        {
            public StopTail(string filePath)
            {
                FilePath = filePath;
            }

            private string FilePath { get; set; }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                10, // maxNumberOfRetries
                TimeSpan.FromSeconds(30), // withinTimeRange
                x => // localOnlyDecider
                {
                    return x switch
                    {
                        ArithmeticException => Directive.Resume,
                        NotSupportedException => Directive.Stop,
                        _ => Directive.Restart
                    };
                });
        }
    }
}
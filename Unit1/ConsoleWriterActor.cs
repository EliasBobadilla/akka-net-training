using System;
using Akka.Actor;

namespace Unit1
{
    internal class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case Messages.InputError error:
                {
                    error.Reason.Print(ConsoleColor.Red);
                    break;
                }
                case Messages.InputSuccess success:
                {
                    success.Reason.Print(ConsoleColor.Green);
                    break;
                }
                default:
                    message.ToString().Print();
                    break;
            }
        }
    }
}
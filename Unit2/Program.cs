using System;
using System.Windows.Forms;
using Akka.Actor;

namespace Unit2
{
    internal static class Program
    {
        public static ActorSystem ChartActors;

        [STAThread]
        private static void Main()
        {
            ChartActors = ActorSystem.Create("ChartActors");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
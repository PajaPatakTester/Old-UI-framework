using System;
namespace Framework.Loging
{
    public static class Log
    {
        public static void Error(string message)
        {
            Console.WriteLine($"ERROR [{Time()} UCT+1]: {message}");
        }

        public static void Info(string message)
        {
            Console.WriteLine($"INFO [{Time()} UCT+1]: {message}");
        }

        public static void Warning(string message)
        {
            Console.WriteLine($"WARNING [{Time()} UCT+1]: {message}");
        }

        private static string Time()
        {
            return DateTime.UtcNow.AddHours(1).ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}

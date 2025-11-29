using System;
using System.Collections.Generic;

namespace SmartHomeSystem
{
    public class EventLogger
    {
        private readonly List<string> log = new List<string>();

        public void Log(string message)
        {
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            log.Add(entry);
        }

        public void ShowLog()
        {
            if (log.Count == 0)
            {
                Console.WriteLine("Log is empty.");
                return;
            }

            foreach (var entry in log)
                Console.WriteLine(entry);
        }
    }
}

using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Light : ISmartDevice
    {
        private bool isOn;
        private string brightness;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType == "DayTimeChanged")
            {
                string dayTime = (string)eventData;
                if (dayTime == "Morning" && !isOn)
                {
                    isOn = true;
                    Console.WriteLine("Light turn on.");
                }
                else if (dayTime == "Night" && isOn)
                {
                    isOn = false;
                    Console.WriteLine("Light turn off");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("Brightness"))
            {
                brightness = (string)settings["Brightness"];
            }

            Console.WriteLine($"Light configured: Brightness={brightness}");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Light manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Light manually turned off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Light");
            }
        }
    }
}

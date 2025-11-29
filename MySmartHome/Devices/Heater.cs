using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Heater : ISmartDevice
    {
        private int minTemperature = 10;
        private bool isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType == "TemperatureChanged")
            {
                int temperature = (int)eventData;
                if (temperature < minTemperature && !isOn)
                {
                    isOn = true;
                    Console.WriteLine("Heater turned on.");
                }
                else if (temperature >= minTemperature && isOn)
                {
                    isOn = false;
                    Console.WriteLine("Heater turned off.");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("minTemperature"))
                minTemperature = (int)settings["minTemperature"];

            Console.WriteLine($"Heater configued: Min={minTemperature}°C.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Heater manually turn on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Heater manually turn off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Heater.");
            }
        }
    }
}

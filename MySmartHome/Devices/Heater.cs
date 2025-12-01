using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Heater : ISmartDevice
    {
        private readonly EventLogger logger = new EventLogger();
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
                    logger.Log("Heater turned on.");
                }
                else if (temperature >= minTemperature && isOn)
                {
                    isOn = false;
                    logger.Log("Heater turned off.");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("minTemperature"))
                minTemperature = (int)settings["minTemperature"];

            logger.Log($"Heater configued: Min={minTemperature}°C.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                logger.Log("Heater manually turn on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                logger.Log("Heater manually turn off.");
            }
            else
            {
                logger.Log("Invalid command for Heater.");
            }
        }
    }
}

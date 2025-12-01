using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class AirConditioner : ISmartDevice
    {
        private readonly EventLogger logger = new EventLogger();
        private int minTemperature = 18;
        private int maxTemperature = 25;
        private bool isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType == "TemperatureChanged")
            {
                int temperature = (int)eventData;
                if (temperature > maxTemperature && !isOn)
                {
                    isOn = true;
                    logger.Log("Air Conditioner turned on (High Temperature).");
                }
                else if (temperature < minTemperature && isOn)
                {
                    isOn = false;
                    logger.Log("Air Conditioner turned off (Low Temperature).");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("MinTemperature"))
                minTemperature = (int)settings["MinTemperature"];
            if (settings.ContainsKey("MaxTemperature"))
                maxTemperature = (int)settings["MaxTemperature"];

            logger.Log($"Air Conditioner configured: Min={minTemperature}°C, Max={maxTemperature}°C.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                logger.Log("Air Conditioner manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                logger.Log("Air Conditioner manually turned off.");
            }
            else
            {
                logger.Log("Invalid command for Air Conditioner.");
            }
        }
    }
}

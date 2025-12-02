using System;
using System.Collections.Generic;
using SmartHomeSystem;

namespace MySmartHome.Devices
{
    public class Light : ISmartDevice
    {
        private readonly EventLogger logger = new EventLogger();
        private bool isOn;
        private string brightness;
        public string Name { get; set;} = "Light";

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType == "DayTimeChanged")
            {
                string dayTime = (string)eventData;
                if (dayTime == "Morning" && !isOn)
                {
                    isOn = true;
                    logger.Log("Light turned on.");
                }
                else if (dayTime == "Night" && isOn)
                {
                    isOn = false;
                    logger.Log("Light turned off");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("Brightness"))
            {
                brightness = (string)settings["Brightness"];
            }

            logger.Log($"Light configured: Brightness={brightness}");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                logger.Log("Light manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                logger.Log("Light manually turned off.");
            }
            else
            {
                logger.Log("Invalid command for Light");
            }
        }
    }
}

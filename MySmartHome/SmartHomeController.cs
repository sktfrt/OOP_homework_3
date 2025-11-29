using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Generic;
using MySmartHome.Devices;

namespace SmartHomeSystem
{
    public class SmartHomeController
    {
        public event Action<string> OnDayTimeChanged;
        public event Action<int> OnTemperatureChanged;
        public event Action OnMotionDetected;

        private readonly Dictionary<string, ISmartDevice> devices = new();
        private readonly EventLogger logger = new EventLogger();

        public void RegisterDevice(string name, ISmartDevice device)
        {
            devices[name] = device;
        }

        public void ChangeDayTime(string timeOfDay)
        {
            Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
            logger.Log($"Daytime changed to {timeOfDay}.");
            OnDayTimeChanged?.Invoke(timeOfDay);
        }

        public void ChangeTemperature(int temperature)
        {
            Console.WriteLine($"Event: Temperature changed to {temperature}");
            logger.Log($"Temperature changed to {temperature}");
            OnTemperatureChanged?.Invoke(temperature);
        }

        public void DetectMotion()
        {
            Console.WriteLine("Event: Motion detected");
            logger.Log("Motion detected");
            OnMotionDetected?.Invoke();
        }

        public void TriggerDevice(string deviceName, string command)
        {
            if (!devices.ContainsKey(deviceName))
            {
                logger.Log($"Device '{deviceName}' not found.");
                return;
            }

            var device = devices[deviceName];
            device.ExecuteCommand(command);
            logger.Log($"Device '{deviceName}' executed command '{command}'.");
        }

        public void ShowLog()
        {
            logger.ShowLog();
        }
    }
}

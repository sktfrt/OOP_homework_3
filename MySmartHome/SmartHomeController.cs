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
            if (devices.ContainsKey(name))
            {
                logger.Log($"Device {name} already exist\n");
                return;
            }

            devices[name.ToLower()] = device;
        }

        public void ChangeDayTime(string timeOfDay)
        {
            logger.Log($"Event: Daytime changed to {timeOfDay}.");
            logger.Log($"Daytime changed to {timeOfDay}.");

            var handlers = OnDayTimeChanged?.GetInvocationList();
            if (handlers == null) return;

            foreach (var handler in handlers)
            {
                try
                {
                    ((Action<string>)handler).Invoke(timeOfDay);
                }
                catch (Exception e)
                {
                    logger.Log($"Handler error for ChangeDayTime: {e.Message}");
                }
            }

        }

        public void ChangeTemperature(int temperature)
        {
            logger.Log($"Event: Temperature changed to {temperature}");
            logger.Log($"Temperature changed to {temperature}");
            
            var handlers = OnTemperatureChanged?.GetInvocationList();
            if (handlers == null) return;

            foreach (var handler in handlers)
            {
                try
                {
                    ((Action<int>)handler).Invoke(temperature);
                }
                catch (Exception e)
                {
                    logger.Log($"Handler error for ChangeTemperature: {e.Message}");
                }
            }
        }

        public void DetectMotion()
        {
            logger.Log("Event: Motion detected");
            logger.Log("Motion detected");
            
            var handlers = OnMotionDetected?.GetInvocationList();
            if (handlers == null) return;

            foreach (var handler in handlers)
            {
                try
                {
                    ((Action)handler).Invoke();
                }
                catch (Exception e)
                {
                    logger.Log($"Handler error for DetectMotion: {e.Message}");
                }
            }
        }

        public void TriggerDevice(string deviceName, string command)
        {
            deviceName = deviceName.ToLower();
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

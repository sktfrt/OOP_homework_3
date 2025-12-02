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
        public event Action<bool> OnMotionDetected;

        private readonly List<ISmartDevice> devices = new();
        private readonly EventLogger logger = new EventLogger();


        public void RegisterDevice(ISmartDevice device)
        {
            devices.Add(device);
        }

        private void SafeInvoke<T>(MulticastDelegate multicast, T arg, string eventName)
        {
            if (multicast == null) return;

            foreach (var handler in multicast.GetInvocationList())
            {
                try
                {
                    ((Action<T>)handler).Invoke(arg);
                }
                catch (Exception e)
                {
                    logger.Log($"Handler error in {eventName}: {e.Message}");
                }
            }
        }

        public void ChangeDayTime(string timeOfDay)
        {
            logger.Log($"Event: Daytime changed to {timeOfDay}.");
            logger.Log($"Daytime changed to {timeOfDay}.");

            SafeInvoke(OnDayTimeChanged, timeOfDay, nameof(ChangeDayTime));
        }

        public void ChangeTemperature(int temperature)
        {
            logger.Log($"Event: Temperature changed to {temperature}");
            logger.Log($"Temperature changed to {temperature}");

            SafeInvoke(OnTemperatureChanged, temperature, nameof(ChangeTemperature));
        }

        public void DetectMotion(bool detected)
        {
            logger.Log("Event: Motion detected");
            logger.Log("Motion detected");

            SafeInvoke(OnMotionDetected, detected, nameof(DetectMotion));
        }

        public void TriggerDevice(string deviceName, string command)
        {
            var device = devices
                .FirstOrDefault(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));

            if (device == null)
            {
                logger.Log($"Device '{deviceName}' not found.\nRegistered devices: AirConditioner, Heater, Light");
                return;
            }

            device.ExecuteCommand(command);
            logger.Log($"Device '{deviceName}' executed '{command}'.");
        }

        public void ShowLog()
        {
            logger.ShowLog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Device.Pwm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Utilities
{
    public class Gpios
    {
        /// <summary>
        /// Pi GPIO Controller
        /// </summary>
        public static GpioController piGPIOController { get; set; }

        public Gpios(GpioController controller)
        {
            piGPIOController = controller;

            headLight.Start();
            footLight.Start();

            piGPIOController.OpenPin(InputTrigger, PinMode.Input);
            piGPIOController.OpenPin(Blinders, PinMode.Output);
            piGPIOController.OpenPin(Fan, PinMode.Output);
            piGPIOController.OpenPin(Smoke, PinMode.Output);
            piGPIOController.OpenPin(Status, PinMode.Output);
            piGPIOController.OpenPin(Eyes, PinMode.Output);
     

        }

        /// <summary>
        /// Foot peddal Trigger (Red Wire)
        /// </summary>
        public static int InputTrigger { get; } = 4;

        /// <summary>
        /// Bright red lights at the back of the carrage (Blue Wire)
        /// </summary>
        public static int Blinders { get; } = 27;

        /// <summary>
        /// Fan for smoke (White Wire)
        /// </summary>
        public static int Fan { get; } = 17;

        /// <summary>
        /// Smoke Relay (Black Wire)
        /// </summary>
        public static int Smoke { get; } = 22;

        /// <summary>
        /// Smoke Relay (Black Wire)
        /// </summary>
        public static int Status { get; } = 5;

        /// <summary>
        /// Red Ligths in the Eyes, real creapy
        /// </summary>
        public static int Eyes { get; } = 14;

        /// <summary>
        /// PI-GPIO-12 Head Lights(PWM Dimmable Channel 1) (Blue Wire)
        /// </summary>
        public static PwmChannel headLight = PwmChannel.Create(0, 0, 400, 0.001);



        /// <summary>
        /// PI-GPIO-13 Foot Lights(PWM Dimmable Channel 1) (Yellow Wire)
        /// </summary>
        public static PwmChannel footLight = PwmChannel.Create(0, 1, 400, 0.001);



    }
}

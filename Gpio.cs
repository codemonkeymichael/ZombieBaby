using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby
{
    public class Gpios
    {
        /// <summary>
        /// Foot peddal Trigger
        /// </summary>
        public static int InputTrigger { get; } = 4;

        /// <summary>
        /// Bright red lights at the back of the carrage
        /// </summary>
        public static int Blinders { get; } = 27;

   
    }
}

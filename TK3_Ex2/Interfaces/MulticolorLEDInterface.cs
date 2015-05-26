using System;
using Microsoft.SPOT;
using TK3_Ex2.Definitions;

namespace TK3_Ex2.Interfaces
{
    /// <summary>
    /// Interface defines the interaction with a LED Multicolor module
    /// </summary>
    interface MulticolorLEDInterface
    {
        /// <summary>
        /// Let the LED blink endlessly in one of the definied colors
        /// </summary>
        /// <param name="color">Defines the color the LED should have</param>
        void startBlinking(LEDColor color);
        
        /// <summary>
        /// Stops the blinking of an active LED
        /// </summary>
        void stopBlinking();
    }
}

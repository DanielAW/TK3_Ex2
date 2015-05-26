using System;
using Microsoft.SPOT;
using TK3_Ex2.Definitions;

namespace TK3_Ex2.Interfaces
{
    /// <summary>
    /// Defines an interface that can be used to draw on the display
    /// </summary>
    interface DisplayInterface
    {
        /// <summary>
        /// Defines a method that can be used to draw a string in the standard fond and size on the display
        /// </summary>
        /// <param name="text">The text that should be written</param>
        /// <param name="color">The color in which the text should appear</param>
        /// <param name="xPosition">The horizontal position the text should start</param>
        /// <param name="yPosition">The vertical position the text should start</param>
        void printText(string text, DisplayColors color, int xPosition, int yPosition);
        
        /// <summary>
        /// Prints a round dot with a 1px black border
        /// </summary>
        /// <param name="color">The color in which the dot should appear</param>
        /// <param name="xPosition">The horizontal position the text should start</param>
        /// <param name="yPosition">The vertical position the text should start</param>
        /// <param name="size">The size  of the dot</param>
        void printDot(GameColors color, int xPosition, int yPosition, int size);


        /// <summary>
        /// Prints a round dot with a 1px black border
        /// </summary>
        /// <param name="color">The color in which the dot should appear</param>
        /// <param name="xPosition">The horizontal position the text should start</param>
        /// <param name="yPosition">The vertical position the text should start</param>
        /// <param name="size">The size  of the dot</param>
        void printDot(DisplayColors color, int xPosition, int yPosition, int size);

        /// <summary>
        /// Prints a rectangle
        /// </summary>
        /// <param name="text">The text that should be written</param>
        /// <param name="color">The color in which the rectangle should appear</param>
        /// <param name="xPosition">The horizontal position the rectangle should start</param>
        /// <param name="yPosition">The vertical position the rectangle should start</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        void printRectangle(DisplayColors color, int xPosition, int yPosition, int width, int height);

        /// <summary>
        /// Prints a rectangle with the defined text
        /// </summary>
        /// <param name="text">The text that should be written</param>
        /// <param name="color">The color in which the rectangle should appear</param>
        /// <param name="xPosition">The horizontal position the rectangle should start</param>
        /// <param name="yPosition">The vertical position the rectangle should start</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        void printTextRectangle(string text, DisplayColors color, int xPosition, int yPosition, int width, int height);

        /// <summary>
        /// Clears the Display
        /// </summary>
        void clearDisplay();

        /// <summary>
        /// Sets the background of the Display to the defined color
        /// </summary>
        /// <param name="color">The color the background should have</param>
        void setBackgroundColor(DisplayColors color);
    }
}

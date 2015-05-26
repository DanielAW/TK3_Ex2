using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using TK3_Ex2.Interfaces;
using TK3_Ex2.Definitions;
using TK3_Ex2.Classes;

namespace TK3_Ex2
{
    /// <summary>
    /// The auto generated main class of the program
    /// </summary>
    public partial class Program : MulticolorLEDInterface, DisplayInterface
    {
        //the sensitivity for the joystick yaw detection
        private const double joystickSensitivity = 0.6;

        //the sensitivity for the joystick center position detection
        private const double joystickCenterSensitivity = 0.3;

        //timer for checking the joystick position
        GT.Timer joystickTimer; 

        //Checkintervall for the joystick
        private const int joystickCheckInterval = 400;

        //default font used for all text output
        private Font displayFont = Resources.GetFont(Resources.FontResources.NinaB);

        //the last detected position of the joystick
        JoystickPosition lastPosition;

        //Controller of the game
        ControlInterface controller;

        /// <summary>
        /// This method is run when the mainboard is powered up or reset.   
        /// </summary>
        void ProgramStarted()
        {
            //init the button event handler
            button.ButtonPressed += button_ButtonPressed;

            //init a timer and event handler to check the joystick
            joystick.Calibrate();
            lastPosition = JoystickPosition.Center;
            joystickTimer = new GT.Timer(joystickCheckInterval);
            joystickTimer.Tick += timer_Tick;
            joystick.JoystickPressed += joystick_JoystickPressed;
            joystickTimer.Start();

            controller = new GameController(this, this);

        }

        /// <summary>
        /// Handles the case that the joystick was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="state"></param>
        void joystick_JoystickPressed(Joystick sender, Joystick.ButtonState state)
        {
            controller.buttonPressed();
        }

        /// <summary>
        /// Event handler for the case that the button was pressed
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="state">State the button is in</param>
        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            controller.buttonPressed();
        }



        /// <summary>
        /// Event hander for the case our joystick check timer expired
        /// </summary>
        /// <param name="timer">The check timer for the joystick</param>
        void timer_Tick(GT.Timer timer)
        {
            //Init the position to a invalid position for debugging
            JoystickPosition position = JoystickPosition.Invalid;


            //Joystick is pushed left
            if (joystick.GetPosition().X < -joystickSensitivity)
            {
                position = JoystickPosition.Left;
            }

                //Joystick is pushed right
            else if (joystick.GetPosition().X > joystickSensitivity)
            {
                position = JoystickPosition.Right;            
            }

            //Joystick is pushed down
            if (joystick.GetPosition().Y < -joystickSensitivity)
            {
                position = JoystickPosition.Down;
            }

            //Joystick is pushed up
            else if (joystick.GetPosition().Y > joystickSensitivity)
            {
                position = JoystickPosition.Up;          
            }

            //Joystick is in center position
            else if (joystick.GetPosition().X < joystickCenterSensitivity && joystick.GetPosition().X > -joystickCenterSensitivity 
                && joystick.GetPosition().Y < joystickCenterSensitivity && joystick.GetPosition().Y > -joystickCenterSensitivity)
            {
                position = JoystickPosition.Center;
            }

            //only send an update if the position has changed
            if (lastPosition != position)
            {
                lastPosition = position;

                controller.joystickMoved(position);
            }
        }

        /// <summary>
        /// Implements the start blinking method, turns the blinking of the multicolor LED on, throw an exception if an undefined color is used
        /// </summary>
        /// <param name="color">The color the LED should blink in</param>
        void MulticolorLEDInterface.startBlinking(LEDColor color)
        {
            switch (color)
            {
                case LEDColor.Green:
                    multicolorLED.BlinkRepeatedly(GT.Color.Green);
                    break;
                case LEDColor.Red:
                    multicolorLED.BlinkRepeatedly(GT.Color.Red);
                    break;
                default:
                    throw new NotImplementedException();          
            }
        }

        /// <summary>
        /// Implements the stop blinking method, turns the multicolor LED off
        /// </summary>
        void MulticolorLEDInterface.stopBlinking()
        {
            multicolorLED.TurnOff();
        }

        /// <summary>
        /// Implements the method that can be used to draw a string in the standard fond and size on the display
        /// </summary>
        /// <param name="text">The text that should be written</param>
        /// <param name="color">The color in which the text should appear</param>
        /// <param name="xPosition">The horizontal position the text should start</param>
        /// <param name="yPosition">The vertical position the text should start</param>
        void DisplayInterface.printText(string text, DisplayColors color, int xPosition, int yPosition)
        {
            //output color to be used
            GT.Color printColor;


            //Set the color according to the color scheme
            switch (color)
            {
                case DisplayColors.Black:
                    printColor = GT.Color.Black;
                    break;
                case DisplayColors.Gray:
                    printColor = GT.Color.Gray;
                    break;

                case DisplayColors.LightGrey:
                    printColor = GT.Color.LightGray;
                    break;

                case DisplayColors.White:
                    printColor = GT.Color.White;
                    break;
                default:
                    throw new NotImplementedException();
            }

            displayTE35.SimpleGraphics.DisplayText(text, displayFont, printColor, xPosition, yPosition);
        }

        /// <summary>
        /// Implements the method that prints a round dot with a 1px black border
        /// </summary>
        /// <param name="color">The color in which the dot should appear</param>
        /// <param name="xPosition">The horizontal position the text should start</param>
        /// <param name="yPosition">The vertical position the text should start</param>
        /// <param name="size">The size  of the dot</param>
        void DisplayInterface.printDot(GameColors color, int xPosition, int yPosition, int size)
        {
            //Output color to be used
            GT.Color printColor;


            //Set the color according to the color scheme
            switch (color)
            {
                case GameColors.Blue:
                    printColor = GT.Color.Blue;
                    break;
                case GameColors.Brown:
                    printColor = GT.Color.Brown;
                    break;
                case GameColors.Green:
                    printColor = GT.Color.Green;
                    break;
                case GameColors.Orange:
                    printColor = GT.Color.Orange;
                    break;
                case GameColors.Magenta:
                    printColor = GT.Color.Magenta;
                    break;
                case GameColors.Yellow:
                    printColor = GT.Color.Yellow;
                    break;
                case GameColors.Red:
                    printColor = GT.Color.Red;
                    break;
                case GameColors.White:
                    printColor = GT.Color.White;
                    break;
                case GameColors.LightGrey:
                    printColor = GT.Color.LightGray;
                    break;
                default:
                    throw new NotImplementedException();
            }

            displayTE35.SimpleGraphics.DisplayEllipse(GT.Color.Black, 1, printColor, xPosition, yPosition, size, size);
        }


        /// <summary>
        /// Implements the method that prints a round dot with a 1px black border
        /// </summary>
        /// <param name="color">The color in which the dot should appear</param>
        /// <param name="xPosition">The horizontal position the text should start</param>
        /// <param name="yPosition">The vertical position the text should start</param>
        /// <param name="size">The size  of the dot</param>
        void DisplayInterface.printDot(DisplayColors color, int xPosition, int yPosition, int size)
        {
            //Output color to be used
            GT.Color printColor;


            //Set the color according to the color scheme
            switch (color)
            {
                case DisplayColors.Black:
                    printColor = GT.Color.Black;
                    break;
                case DisplayColors.Gray:
                    printColor = GT.Color.Gray;
                    break;
                case DisplayColors.LightGrey:
                    printColor = GT.Color.LightGray;
                    break;
                case DisplayColors.White:
                    printColor = GT.Color.White;
                    break;
                default:
                    throw new NotImplementedException();
            }

            displayTE35.SimpleGraphics.DisplayEllipse(GT.Color.Black, 1, printColor, xPosition, yPosition, size, size);
        } 

        /// <summary>
        /// Implements the method that prints a rectangle with the defined text inside
        /// </summary>
        /// <param name="text">The text that should be written</param>
        /// <param name="color">The color in which the rectangle should appear</param>
        /// <param name="xPosition">The horizontal position the rectangle should start</param>
        /// <param name="yPosition">The vertical position the rectangle should start</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        void DisplayInterface.printTextRectangle(string text, DisplayColors color, int xPosition, int yPosition, int width, int height)
        {
            //Output color to be used
            GT.Color printColor;


            //Set the color according to the color scheme
            switch (color)
            {
                case DisplayColors.Black:
                    printColor = GT.Color.Black;
                    break;
                case DisplayColors.Gray:
                    printColor = GT.Color.Gray;
                    break;

                case DisplayColors.LightGrey:
                    printColor = GT.Color.LightGray;
                    break;

                case DisplayColors.White:
                    printColor = GT.Color.White;
                    break;
                default:
                    throw new NotImplementedException();
            }

            displayTE35.SimpleGraphics.DisplayTextInRectangle(text, xPosition, yPosition, width, height, printColor, displayFont);
        }

        /// <summary>
        /// Implementation of the method that clears the Display
        /// </summary>
        void DisplayInterface.clearDisplay()
        {
            displayTE35.SimpleGraphics.Clear();
        }

        /// <summary>
        /// Implements the interface to set the background color of the Display
        /// </summary>
        /// <param name="color">The color the background should have</param>
        void DisplayInterface.setBackgroundColor(DisplayColors color)
        {
            //Output color to be used
            GT.Color printColor;


            //Set the color according to the color scheme
            switch (color)
            {
                case DisplayColors.Black:
                    printColor = GT.Color.Black;
                    break;
                case DisplayColors.Gray:
                    printColor = GT.Color.Gray;
                    break;

                case DisplayColors.LightGrey:
                    printColor = GT.Color.LightGray;
                    break;

                case DisplayColors.White:
                    printColor = GT.Color.White;
                    break;
                default:
                    throw new NotImplementedException();
            }

            displayTE35.SimpleGraphics.BackgroundColor = printColor;
        }

        /// <summary>
        /// Implements the method to print a rectangle
        /// </summary>
        /// <param name="text">The text that should be written</param>
        /// <param name="color">The color in which the rectangle should appear</param>
        /// <param name="xPosition">The horizontal position the rectangle should start</param>
        /// <param name="yPosition">The vertical position the rectangle should start</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        void DisplayInterface.printRectangle(DisplayColors color, int xPosition, int yPosition, int width, int height)
        {
            //Output color to be used
            GT.Color printColor;


            //Set the color according to the color scheme
            switch (color)
            {
                case DisplayColors.Black:
                    printColor = GT.Color.Black;
                    break;
                case DisplayColors.Gray:
                    printColor = GT.Color.Gray;
                    break;

                case DisplayColors.LightGrey:
                    printColor = GT.Color.LightGray;
                    break;

                case DisplayColors.White:
                    printColor = GT.Color.White;
                    break;
                default:
                    throw new NotImplementedException();
            }

            displayTE35.SimpleGraphics.DisplayRectangle(printColor, 0, printColor, xPosition, yPosition, width, height);
        }
    }
}

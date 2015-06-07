using System;
using Microsoft.SPOT;

using TK3_Ex2.Interfaces;
using TK3_Ex2.Definitions;

namespace TK3_Ex2.Classes.Pages
{
    class GameSelectionPage : PageInterface
    {
        /// <summary>
        /// Creates the page on which the player can select the game mode (single or multiplayer)
        /// </summary>
        private DisplayInterface myDisplay;
        private int verticalPosition;
        private const int GAME_MODE_COUNT = 2;

        public GameSelectionPage(DisplayInterface display){
            myDisplay = display;
            verticalPosition = 0;

            this.drawPage();
        }

        // method for drawing the page onto the display
        private void drawPage()
        {
            myDisplay.clearDisplay();
            myDisplay.setBackgroundColor(DisplayColors.White);
            myDisplay.printText("Choose the game mode: ", DisplayColors.Black, Constants.HEADER_X_POS, Constants.HEADER_Y_POS);

            this.drawButtons();
        }
        // method for drawing the 2 buttons onto the display (single or multiplayer) 
        private void drawButtons()
        {
            int xPos = Constants.DISPLAY_MIDDLE_X - (Constants.BUTTON_WIDTH / 2) - Constants.SELECTION_MARKER_WIDTH - (Constants.BUTTON_V_DISTANCE/2);
            int yPos = Constants.DISPLAY_MIDDLE_Y - (Constants.BUTTON_HEIGHT / 2) - Constants.SELECTION_MARKER_WIDTH;
            int width = (2 * Constants.SELECTION_MARKER_WIDTH) + Constants.BUTTON_WIDTH;
            int height = (2 * Constants.SELECTION_MARKER_WIDTH) + Constants.BUTTON_HEIGHT;

            if (0 == verticalPosition)
            {
                myDisplay.printRectangle(DisplayColors.Gray, xPos, yPos, width, height);
                yPos = Constants.DISPLAY_MIDDLE_Y + (Constants.BUTTON_HEIGHT / 2) - Constants.SELECTION_MARKER_WIDTH + (Constants.BUTTON_V_DISTANCE / 2);
                myDisplay.printRectangle(DisplayColors.LightGrey, xPos, yPos, width, height);
            }
            else
            {
                myDisplay.printRectangle(DisplayColors.LightGrey, xPos, yPos, width, height);
                yPos = Constants.DISPLAY_MIDDLE_Y + (Constants.BUTTON_HEIGHT / 2) - Constants.SELECTION_MARKER_WIDTH + (Constants.BUTTON_V_DISTANCE / 2);
                myDisplay.printRectangle(DisplayColors.Gray, xPos, yPos, width, height);
            }

            xPos = Constants.DISPLAY_MIDDLE_X - (Constants.BUTTON_WIDTH / 2) - (Constants.BUTTON_V_DISTANCE / 2) + Constants.PRINT_WIDTH ;
            yPos = Constants.DISPLAY_MIDDLE_Y - (Constants.BUTTON_HEIGHT / 2) - (Constants.BUTTON_V_DISTANCE / 2) + Constants.PRINT_HEIGHT *2;
            width = Constants.BUTTON_WIDTH;
            height =  Constants.BUTTON_HEIGHT;

            myDisplay.printTextRectangle("Singleplayer", DisplayColors.White, xPos, yPos, width, height);

            yPos = Constants.DISPLAY_MIDDLE_Y + (Constants.BUTTON_HEIGHT / 2) + (Constants.BUTTON_V_DISTANCE / 2) + Constants.PRINT_HEIGHT;

            myDisplay.printTextRectangle("Multiplayer", DisplayColors.White, xPos, yPos, width, height);
        }

        // Method for selecting the buttons when the joystick is touched
        void PageInterface.joystickMove(Definitions.JoystickPosition position)
        {
            switch (position)
            {
                case JoystickPosition.Down:
                case JoystickPosition.Up:
                    verticalPosition = ((verticalPosition + 1 ) % GAME_MODE_COUNT);
                    this.drawButtons();
                    break;
            }
        }

        // Method for jumping to the selected page when the button is pressed
        GameStates PageInterface.buttonPressed()
        {
            if (0 == verticalPosition)
            {
                return GameStates.CodeBreaking;
            } else
            {
                return GameStates.CodeSelection;
            }
        }
    }
}

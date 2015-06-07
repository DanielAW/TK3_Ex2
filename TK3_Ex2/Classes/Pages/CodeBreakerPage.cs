using System;
using Microsoft.SPOT;

using TK3_Ex2.Interfaces;
using TK3_Ex2.Definitions;


namespace TK3_Ex2.Classes.Pages
{
    /// <summary>
    /// Creates the page on which the player can try to break the code
    /// </summary>
    class CodeBreakerPage : PageInterface
    {
        //The logic that contains the main game mechanism
        private GameLogic logic;

        //Display for the output
        private DisplayInterface myDisplay;

        //The last code the user has selected
        private Code guessedCode;

        //The code the user has to guess
        private Code configuredCode;

        //Counter for the number of codes the user has already guessed
        private int line;

        //The horizontal position of the user selection
        private int horizontalPosition;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newCode">The code that has to be guessed</param>
        /// <param name="display">The output screen</param>
        public CodeBreakerPage(Code newCode, DisplayInterface display)
        {
            horizontalPosition = 0;
            myDisplay = display;
            configuredCode = newCode;  
            logic = new GameLogic(newCode);
            guessedCode = new Code();

            //display the starting picture
            this.drawPage();
        }

        /// <summary>
        /// Draws the basic screen that is shown at the beginning
        /// </summary>
        private void drawPage()
        {
            //Reset the display and add the background color
            myDisplay.clearDisplay();
            myDisplay.setBackgroundColor(DisplayColors.White);

            //writes the header line
            myDisplay.printText("Try to guess the code: ", DisplayColors.Black, Constants.HEADER_X_POS, Constants.HEADER_Y_POS);

            //prints the first horizontal position as the selected dot
            drawSelectedDot(0);

            //prints all not selected dots
            for (int i = 1; i < Constants.COLUMNS; i++)
            {
                drawDeselectedDot(i);
            }

        }

        /// <summary>
        /// Used to write a string telling the player if he has guessed correctly or has lost the game
        /// </summary>
        /// <param name="text"></param>
        private void writeResult(string text)
        {
            myDisplay.printText(text , DisplayColors.Black, Constants.CONFIG_TEXT_X_START, Constants.CONFIG_CODE_Y_START);

            int yPos = Constants.CONFIG_CODE_Y_START + Constants.CONFIG_RESULT_MESSAGE_Y_DIFF;
            int xPos = Constants.CONFIG_CODE_X_START;

            GameColors[] color = configuredCode.getGameColors();

            for(int i = 0; i < Constants.COLUMNS; i++){
                myDisplay.printDot(color[i],(xPos + (i * Constants.DOT_COLUMN_SPACE)),yPos, Constants.DOT_SIZE);
            }
        }

        /// <summary>
        /// method for drawing the result dots
        /// </summary>
        /// <param name="result"></param>
        private void drawResultDots(int[] result)
        {
            int xPos = Constants.SMALL_RESULT_X_START;
            int yPos = Constants.SMALL_RESULT_Y_START + (line * Constants.SMALL_DOT_ROW_SPACE);      

            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myDisplay.printDot((GameColors) result[i], (xPos + (i * Constants.SMALL_RESULT_COLUMN_SPACE)), yPos, Constants.SMALL_RESULT_SIZE);
            }
        }

        //method for creating a new line
        private void writeNewLine()
        {
            drawSelectedDot(0);

            for (int i = 1; i < Constants.COLUMNS; i++)
            {
                drawDeselectedDot(i);
            }
        }

        //method for drawing the selected dot
         private void drawSelectedDot(int position)
        {
            int xPos = Constants.SMALL_DOT_X_START + position * Constants.SMALL_DOT_COLUMN_SPACE - (Constants.SMALL_DOT_DIFF / 2);
            int yPos = line * Constants.SMALL_DOT_ROW_SPACE + Constants.SMALL_DOT_Y_START;
            GameColors color = guessedCode.getGameColorAtPosition(position);

            myDisplay.printDot(DisplayColors.Black, xPos, yPos, Constants.SMALL_DOT_SIZE_SELECTED);

            xPos = Constants.SMALL_DOT_X_START + position * Constants.SMALL_DOT_COLUMN_SPACE;

            myDisplay.printDot(color, xPos-1 , yPos-1 , Constants.SMALL_DOT_SIZE);
        }

         //method for drawing the deselected dot
         private void drawDeselectedDot(int position)
         {
             int xPos = Constants.SMALL_DOT_X_START + position * Constants.SMALL_DOT_COLUMN_SPACE - (Constants.SMALL_DOT_DIFF / 2);
             int yPos = (line * Constants.SMALL_DOT_ROW_SPACE) + Constants.SMALL_DOT_Y_START;
             GameColors color = guessedCode.getGameColorAtPosition(position);

             myDisplay.printDot(DisplayColors.White, xPos, yPos, Constants.SMALL_DOT_SIZE_SELECTED);

             xPos = Constants.SMALL_DOT_X_START + position * Constants.SMALL_DOT_COLUMN_SPACE;

             myDisplay.printDot(color, xPos-1, yPos-1, Constants.SMALL_DOT_SIZE);
         }

         //method for reacting to joystick movement
         void PageInterface.joystickMove(Definitions.JoystickPosition position)
        {
            int lastPosition = horizontalPosition;

            switch (position)
            {
                case JoystickPosition.Up:
                    guessedCode.decrementCodeAtPosition(horizontalPosition);
                    drawSelectedDot(horizontalPosition);
                    break;

                case JoystickPosition.Down:
                    guessedCode.incrementCodeAtPosition(horizontalPosition);
                    drawSelectedDot(horizontalPosition);
                    break;

                case JoystickPosition.Left:
                    horizontalPosition = (--horizontalPosition + Constants.COLUMNS) % Constants.COLUMNS;
                    drawSelectedDot(horizontalPosition);
                    drawDeselectedDot(lastPosition);
                    break;

                case JoystickPosition.Right:
                    horizontalPosition = ((horizontalPosition + 1) % Constants.COLUMNS);
                    drawSelectedDot(horizontalPosition);
                    drawDeselectedDot(lastPosition);
                    break;
            }
        }

         //method for reacting to a pressed button, including the statement whether the game was won or lost
        GameStates PageInterface.buttonPressed()
        {
            int[] resultFields = logic.compareCode(guessedCode);
            bool result = logic.rightGuess(guessedCode);
            drawDeselectedDot(horizontalPosition);
            drawResultDots(resultFields);
            line++;

            if (!result && (line) == Constants.ROWS)
            {   
                writeResult("You have lost the game!");
                return GameStates.GameLost;
            }

            if(result)
            {
                writeResult("You have won!");
                return GameStates.GameWon;
            }

            guessedCode.setToDefault();
            horizontalPosition = 0;
            writeNewLine();
            return GameStates.CodeBreaking;
        }
    }
}

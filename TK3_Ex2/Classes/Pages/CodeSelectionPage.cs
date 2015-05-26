using System;
using Microsoft.SPOT;

using TK3_Ex2.Interfaces;
using TK3_Ex2.Definitions;

namespace TK3_Ex2.Classes.Pages
{
    class CodeSelectionPage : PageInterface
    {
        private Code myCode;
        private DisplayInterface myDisplay;
        private int horizontalPosition;
        private int leftBorder;
        private int middle;

        public CodeSelectionPage(Code newCode, DisplayInterface display)
        {
            myCode = newCode;
            myDisplay = display;
            horizontalPosition = 0;
            leftBorder = Constants.DISPLAY_MIDDLE_X - (int) ((Constants.COLUMNS / 2.0) *  (Constants.DOT_COLUMN_SPACE));
            middle = Constants.DISPLAY_MIDDLE_Y;

            this.drawPage();
        }

        private void drawPage()
        {
            myDisplay.clearDisplay();
            myDisplay.setBackgroundColor(DisplayColors.White);
            myDisplay.printText("Input a new Code: ", DisplayColors.Black, Constants.HEADER_X_POS, Constants.HEADER_Y_POS);

            drawSelectedDot(0);

            for (int i = 1; i < Constants.COLUMNS; i++)
            {
                drawDeselectedDot(i);
            }
        }

        private void drawSelectedDot(int position)
        {
            int xPos = leftBorder + position * Constants.DOT_COLUMN_SPACE - (Constants.DOT_DIFF / 2);
            GameColors color = myCode.getGameColorAtPosition(position);

            myDisplay.printDot(DisplayColors.Black, xPos, middle, Constants.DOT_SIZE_SELECTED);

            xPos = leftBorder + position * Constants.DOT_COLUMN_SPACE;

            myDisplay.printDot(color, xPos-1, middle-1, Constants.DOT_SIZE);
        }

        private void drawDeselectedDot(int position)
        {
            int xPos = leftBorder + position * Constants.DOT_COLUMN_SPACE - (Constants.DOT_DIFF / 2);
            GameColors color = myCode.getGameColorAtPosition(position);

            myDisplay.printDot(DisplayColors.White, xPos, middle, Constants.DOT_SIZE_SELECTED);

            xPos = leftBorder + position * Constants.DOT_COLUMN_SPACE;

            myDisplay.printDot(color, xPos-1, middle-1, Constants.DOT_SIZE);
        }

        void PageInterface.joystickMove(Definitions.JoystickPosition position)
        {
            int lastPosition = horizontalPosition;

            switch (position)
            {
                case JoystickPosition.Up:
                    myCode.decrementCodeAtPosition(horizontalPosition);
                    drawSelectedDot(horizontalPosition);
                    break;

                case JoystickPosition.Down:
                    myCode.incrementCodeAtPosition(horizontalPosition);
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

       GameStates PageInterface.buttonPressed()
        {
            return GameStates.CodeBreaking;
        }
    }
}

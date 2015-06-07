using System;
using Microsoft.SPOT;

using TK3_Ex2.Definitions;

namespace TK3_Ex2.Classes
{
    class Code
    {
        /// <summary>
        /// Class handling the controlling of the code
        /// </summary>

        private int[] myCode;

        public Code()
        {
            myCode = new int[Constants.COLUMNS];
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] = 0;
            }
        }

        // method for setting a new code
        public Code(int[] newCode)
        {
            myCode = new int[Constants.COLUMNS];
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] = newCode[i];
            }
        }

        // method for setting the code to default values (00000)
        public void setToDefault()
        {
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] = 0;
            }
        }

        // method for setting the code to random values 
        public void setCodeToRandomValue()
        {
            myCode = new int[Constants.COLUMNS];
            Random rand = new Random();

            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] =  (rand.Next(Constants.CODE_SIZE));
            }
        }

        // method incrementing the code at the current position
        public void incrementCodeAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
            {
                myCode[position] = (++myCode[position]) % Constants.CODE_SIZE;
            }
        }

        // method decrementing the code at the current position
        public void decrementCodeAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
            {
                myCode[position] = (--myCode[position] + Constants.CODE_SIZE) % Constants.CODE_SIZE;
            }
        }

        // method returing the code at the current position
        public int getCodeAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return myCode[position];
            else
                return -1;
        }

        // method returing the gamecolor at the current position
        public GameColors getGameColorAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return (GameColors) myCode[position];
            else
                return GameColors.White;
        }

        // method returing the gamecolors
        public GameColors[] getGameColors()
        {
            GameColors[] colors = new GameColors[Constants.COLUMNS];

            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                colors[i] = (GameColors) myCode[i];
            }

            return colors;
        }

        // method to compare the current position in the code with the saved code value
        public bool compareWithCodeAtPosition(int position, int code)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return (myCode[position] == code);
            else
                return false;
        }

        // method to compare the current position in the code with the saved code value
        public bool compareWithCodeAtPosition(int position, Code compareCode)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return (myCode[position] == compareCode.getCodeAtPosition(position));
            else
                return false;
        }
    }
}

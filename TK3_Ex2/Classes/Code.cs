using System;
using Microsoft.SPOT;

using TK3_Ex2.Definitions;

namespace TK3_Ex2.Classes
{
    class Code
    {
        
        private int[] myCode;

        public Code()
        {
            myCode = new int[Constants.COLUMNS];
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] = 0;
            }
        }

        public Code(int[] newCode)
        {
            myCode = new int[Constants.COLUMNS];
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] = newCode[i];
            }
        }

        public void setToDefault()
        {
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] = 0;
            }
        }

        public void setCodeToRandomValue()
        {
            myCode = new int[Constants.COLUMNS];
            Random rand = new Random();

            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                myCode[i] =  (rand.Next(Constants.CODE_SIZE));
            }
        }

        public void incrementCodeAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
            {
                myCode[position] = (++myCode[position]) % Constants.CODE_SIZE;
            }
        }

        public void decrementCodeAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
            {
                myCode[position] = (--myCode[position] + Constants.CODE_SIZE) % Constants.CODE_SIZE;
            }
        }

        public int getCodeAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return myCode[position];
            else
                return -1;
        }

        public GameColors getGameColorAtPosition(int position)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return (GameColors) myCode[position];
            else
                return GameColors.White;
        }

        public GameColors[] getGameColors()
        {
            GameColors[] colors = new GameColors[Constants.COLUMNS];

            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                colors[i] = (GameColors) myCode[i];
            }

            return colors;
        }

        public bool compareWithCodeAtPosition(int position, int code)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return (myCode[position] == code);
            else
                return false;
        }

        public bool compareWithCodeAtPosition(int position, Code compareCode)
        {
            if (position < Constants.COLUMNS && position >= 0)
                return (myCode[position] == compareCode.getCodeAtPosition(position));
            else
                return false;
        }
    }
}

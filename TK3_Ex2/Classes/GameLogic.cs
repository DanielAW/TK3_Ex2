using System;
using Microsoft.SPOT;

using TK3_Ex2.Definitions;

namespace TK3_Ex2.Classes
{
    /// <summary>
    /// The class containing all of the games logic
    /// </summary>
    class GameLogic
    {
        private Code currentCode;
        private const int NO_HIT = (int) GameColors.LightGrey;
        private const int RIGHT_COLOR = (int) GameColors.White;
        private const int RIGHT_POSITION = (int) GameColors.Red;

        public GameLogic(Code code)
        {
            currentCode = code;
        }

        // method for comparing the guessed code and the saved code
        public int[] compareCode(Code guessedCode)
        {
            int rightPosition = 0;
            int rightColor = 0;
            int[] result = new int[Constants.COLUMNS];

            for (int i = 0; Constants.COLUMNS > i; i++)
            {
                if (guessedCode.compareWithCodeAtPosition(i, currentCode))
                {
                    rightPosition++;
                }
                else
                {
                    for (int j = 0; Constants.COLUMNS > j; j++)
                    {
                        if (guessedCode.compareWithCodeAtPosition(i, currentCode.getCodeAtPosition(j)) && !guessedCode.compareWithCodeAtPosition(j, currentCode))
                        {
                            bool secondAppearance = false;
                            if (i > 0)
                            {
                                for (int k = 0; k < i; k++)
                                {
                                    if (guessedCode.compareWithCodeAtPosition(i, guessedCode.getCodeAtPosition(k)))
                                    {
                                        secondAppearance = true;
                                    }
                                }
                            }
                            if(!secondAppearance){
                                rightColor++;
                            }
                                
                            break;
                        }
                    }
                }           
            }

            int index = 0;
            
            for (; index < rightPosition ; index++)
            {
                result[index] = RIGHT_POSITION;
            }

            int next = index + rightColor;
            for (; index < next; index++)
            {
                result[index] = RIGHT_COLOR;
            }

            for (; index < Constants.COLUMNS; index++)
            {
                result[index] = NO_HIT;
            }

            return result;
        }

        // method returning true if the guess was right
        public bool rightGuess(Code guessedCode)
        {  
            for (int i = 0; i < Constants.COLUMNS; i++)
            {
                if (!currentCode.compareWithCodeAtPosition(i, guessedCode))
                {
                    return false;
                }
                    
            }

                return true;
        }
    }
}

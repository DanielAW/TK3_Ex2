using System;
using Microsoft.SPOT;

using TK3_Ex2.Interfaces;
using TK3_Ex2.Classes.Pages;
using TK3_Ex2.Definitions;
using TK3_Ex2.Classes;

namespace TK3_Ex2.Classes
{
    /// <summary>
    /// Class containing the game controller 
    /// </summary>
    class GameController :  ControlInterface
    {
        private PageInterface currentPage;
        private Code currentCode;
        private GameStates currentState;
        private MulticolorLEDInterface myLED;
        private DisplayInterface myDisplay;

        public GameController(MulticolorLEDInterface led, DisplayInterface display)
        {
            myLED = led;
            myDisplay = display;
            currentPage = new GameSelectionPage(myDisplay);
            currentState = GameStates.GameSelection;

        }


        public void joystickMoved(JoystickPosition position)
        {
             currentPage.joystickMove(position);
        }

        public void joystickPressed()
        {
           // not needed here
        }

        // method handling the pressed button event (e.g. what page/screen to load next)
        public void buttonPressed()
        {
            switch(currentState)
            {
                case GameStates.GameSelection:
                    currentState = currentPage.buttonPressed();
                    if (currentState == GameStates.CodeSelection)
                    {
                        currentCode = new Code();
                        currentPage = new CodeSelectionPage(currentCode, myDisplay);
                    }
                    else if (currentState == GameStates.CodeBreaking)
                    {
                        currentCode = new Code();
                        currentCode.setCodeToRandomValue();
                        currentPage = new CodeBreakerPage(currentCode, myDisplay);
                    }

                   
                    break;
                case GameStates.CodeSelection:
                    currentState = GameStates.CodeBreaking;
                    currentPage = new CodeBreakerPage(currentCode, myDisplay);
                    break;

                case GameStates.CodeBreaking:
                    currentState = currentPage.buttonPressed();

                    if (GameStates.GameWon == currentState)
                    {
                        myLED.startBlinking(LEDColor.Green);
                    }
                    else if (GameStates.GameLost == currentState)
                    {
                        myLED.startBlinking(LEDColor.Red);
                    }
                    break;
                case GameStates.GameLost:
                case GameStates.GameWon:
                    myLED.stopBlinking();
                    currentState = GameStates.GameSelection;
                    currentPage = new GameSelectionPage(myDisplay);
                    break;
                default:
                    return;
            }
        }
    }
}

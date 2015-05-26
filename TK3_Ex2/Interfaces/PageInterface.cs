using System;
using Microsoft.SPOT;

using TK3_Ex2.Definitions;

namespace TK3_Ex2.Interfaces
{
    interface PageInterface
    {
        
        
       void joystickMove(JoystickPosition position);

        GameStates buttonPressed();
    }
}

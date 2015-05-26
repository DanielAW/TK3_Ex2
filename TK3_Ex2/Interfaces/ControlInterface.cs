using System;
using Microsoft.SPOT;

using TK3_Ex2.Definitions;

namespace TK3_Ex2.Interfaces
{
    interface ControlInterface
    {
        void joystickMoved(JoystickPosition position);

        void joystickPressed();

        void buttonPressed();
    }
}

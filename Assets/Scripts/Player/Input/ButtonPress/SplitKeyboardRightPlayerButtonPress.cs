using System.Collections.Generic;
using UnityEngine;

namespace Player.Input.ButtonPress
{
    public class SplitKeyboardRightPlayerButtonPress : PlayerButtonPress
    {
        protected override void PopulateKeyCodeList()
        {
            KeyCodeList = new KeyCode[]
            {
                KeyCode.RightShift,
                KeyCode.RightAlt,
                KeyCode.Return,
                KeyCode.RightCommand
            } ;
        }
    }
}
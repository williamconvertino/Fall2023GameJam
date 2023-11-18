using System.Collections.Generic;
using UnityEngine;

namespace Player.Input.ButtonPress
{
    public class SpacePlayerButtonPress : PlayerButtonPress
    {
        protected override void PopulateKeyCodeList()
        {
            KeyCodeList = new KeyCode[]
            {
                KeyCode.Space
            } ;
        }
    }
}
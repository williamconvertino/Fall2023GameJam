using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovementInput : PlayerMovementInput
{

    public override MovementInput GetMovementInput()
    {
        return new MovementInput()
        {
            DirX = (Input.GetKey(KeyCode.LeftArrow) ? -1.0f : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0),
            DoJump = Input.GetKeyDown(KeyCode.UpArrow)
        };
    }

    public override TDMovementInput GetTDMovementInput()
    {
        return new TDMovementInput()
        {
            DirX = (Input.GetKey(KeyCode.LeftArrow) ? -1.0f : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0),
            DirY = (Input.GetKey(KeyCode.DownArrow) ? -1.0f : 0) + (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0)
        };
    }
}

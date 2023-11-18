using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedMovementInput : PlayerMovementInput
{
    public override MovementInput GetMovementInput()
    {
        return new MovementInput()
        {
            DirX = Input.GetAxis("Horizontal"),
            DoJump = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)|| Input.GetButton("Jump")
        };
    }
    public override TDMovementInput GetTDMovementInput()
    {
        return new TDMovementInput()
        {
            DirX = Input.GetAxis("Horizontal"),
            DirY = Input.GetAxis("Vertical")
        };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasdMovementInput : PlayerMovementInput
{
    public override MovementInput GetMovementInput()
    {
        return new MovementInput()
        {
            DirX = (Input.GetKey(KeyCode.A) ? -1.0f : 0) + (Input.GetKey(KeyCode.D) ? 1.0f : 0),
            DoJump = Input.GetKeyDown(KeyCode.W)
        };
    }
    public override TDMovementInput GetTDMovementInput()
    {
        return new TDMovementInput()
        {
            DirX = (Input.GetKey(KeyCode.A) ? -1.0f : 0) + (Input.GetKey(KeyCode.D) ? 1.0f : 0),
            DirY = (Input.GetKey(KeyCode.S) ? -1.0f : 0) + (Input.GetKey(KeyCode.W) ? 1.0f : 0),
        };
    }
}

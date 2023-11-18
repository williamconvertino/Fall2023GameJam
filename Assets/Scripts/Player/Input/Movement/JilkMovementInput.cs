using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JilkMovementInput : PlayerMovementInput
{
    public override MovementInput GetMovementInput()
    {
        return new MovementInput()
        {
            DirX = (Input.GetKey(KeyCode.J) ? -1.0f : 0) + (Input.GetKey(KeyCode.L) ? 1.0f : 0),
            DoJump = Input.GetKeyDown(KeyCode.I)
        };
    }
    public override TDMovementInput GetTDMovementInput()
    {
        return new TDMovementInput()
        {
            DirX = (Input.GetKey(KeyCode.J) ? -1.0f : 0) + (Input.GetKey(KeyCode.L) ? 1.0f : 0),
            DirY = (Input.GetKey(KeyCode.K) ? -1.0f : 0) + (Input.GetKey(KeyCode.I) ? 1.0f : 0),
        };
    }
}

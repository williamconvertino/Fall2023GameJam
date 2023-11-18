
using UnityEngine;

public class ControllerAndWASDMovementInput : PlayerMovementInput
{
    public override MovementInput GetMovementInput()
    {
        return new MovementInput()
        {
            DirX = (Input.GetKey(KeyCode.A) ? -1.0f : 0) + (Input.GetKey(KeyCode.D) ? 1.0f : 0)  + Input.GetAxis("Horizontal"),
            DoJump = Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")
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
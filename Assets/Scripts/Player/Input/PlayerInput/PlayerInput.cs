using System;
using Player.Input.ButtonPress;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour 
{
    protected PlayerMovementInput MovementInput { get; set; }
    protected PlayerButtonPress ButtonPress { get; set; }
    protected abstract void Awake();

    public bool ButtonPressed()
    {
        return ButtonPress.ButtonPressed();
    }

    public PlayerMovementInput.MovementInput GetMovementInput()
    {
        return MovementInput.GetMovementInput();
    }

    public PlayerMovementInput.TDMovementInput GetTDMovementInput()
    {
        return MovementInput.GetTDMovementInput();
    }
}
using System;
using Player.Input.ButtonPress;
using UnityEngine;

public class CoopPlayerLeftInput : PlayerInput
{
    protected override void Awake()
    {
        MovementInput = gameObject.AddComponent<WasdMovementInput>();
        ButtonPress = gameObject.AddComponent<SplitKeyboardLeftPlayerButtonPress>();
    }
}
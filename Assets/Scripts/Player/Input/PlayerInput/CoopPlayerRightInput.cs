using System;
using Player.Input.ButtonPress;
using UnityEngine;

public class CoopPlayerRightInput : PlayerInput
{
    protected override void Awake()
    {
        MovementInput = gameObject.AddComponent<ArrowMovementInput>();
        ButtonPress = gameObject.AddComponent<SplitKeyboardRightPlayerButtonPress>();
    }
}
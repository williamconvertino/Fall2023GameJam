using System;
using Player.Input.ButtonPress;
using UnityEngine;

public class CoopPlayerMiddleInput : PlayerInput
{
    protected override void Awake()
    {
        MovementInput = gameObject.AddComponent<JilkMovementInput>();
        ButtonPress = gameObject.AddComponent<SplitKeyboardMiddlePlayerButtonPress>();
    }
}
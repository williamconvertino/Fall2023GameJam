using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementInput : MonoBehaviour
{
    public abstract MovementInput GetMovementInput();
    public abstract TDMovementInput GetTDMovementInput();

    public struct MovementInput
    {
        public float DirX;
        public bool DoJump;
    }
    public struct TDMovementInput
    {
        public float DirX;
        public float DirY;
    }
}

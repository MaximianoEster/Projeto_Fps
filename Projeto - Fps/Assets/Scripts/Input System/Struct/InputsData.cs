using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputsData
{
    public Vector2 KeyboardDirection;
    public Vector2 MouseDirection;
    public bool IsAttacking;
    public bool Isjumping;
    public bool IsAiming;
    public InteractionType InteractionType;
    
    public InputsData(Vector2 keyboardDirection, Vector2 mouseDirection, 
        bool isAttacking, bool isjumping, bool isAiming, InteractionType interactionType)
    {
        KeyboardDirection = keyboardDirection;
        MouseDirection = mouseDirection;
        
        IsAttacking = isAttacking;
        Isjumping = isjumping;
        IsAiming = isAiming;

        InteractionType = interactionType;
    }
}
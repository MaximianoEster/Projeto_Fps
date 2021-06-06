using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputsData
{
    public Vector2 KeyboardDirection;
    public Vector2 MouseDirection;
    public bool IsAttacking;
    public bool Isjumping;
    public InteractionType InteractionType;
    
    public InputsData(Vector2 keyboardDirection, Vector2 mouseDirection, 
        bool isAttacking, bool isjumping, InteractionType interactionType)
    {
        KeyboardDirection = keyboardDirection;
        MouseDirection = mouseDirection;
        
        IsAttacking = isAttacking;
        Isjumping = isjumping;

        InteractionType = interactionType;
    }
}
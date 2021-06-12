using UnityEngine;

public struct InputsData
{
    public Vector2 KeyboardDirection;
    public Vector2 MouseDirection;
    public bool IsAttacking;
    public bool Isjumping;
    public bool IsAiming;
    public bool OpenWeaponWheel;
    public InteractionType InteractionType;
    
    public InputsData(Vector2 keyboardDirection, Vector2 mouseDirection, 
        bool isAttacking, bool isjumping, bool isAiming,bool openWeaponWheel, InteractionType interactionType)
    {
        KeyboardDirection = keyboardDirection;
        MouseDirection = mouseDirection;
        
        IsAttacking = isAttacking;
        Isjumping = isjumping;
        IsAiming = isAiming;
        OpenWeaponWheel = openWeaponWheel;

        InteractionType = interactionType;
    }
}
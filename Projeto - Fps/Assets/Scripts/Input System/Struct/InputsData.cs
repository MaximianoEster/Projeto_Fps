using UnityEngine;

public struct InputsData
{
    public Vector2 KeyboardDirection;
    public Vector2 MouseDirection;
    public bool Isjumping;
    
    public InputsData(Vector2 keyboardDirection, Vector2 mouseDirection, 
        bool isjumping)
    {
        KeyboardDirection = keyboardDirection;
        MouseDirection = mouseDirection;
        Isjumping = isjumping;
    }
}
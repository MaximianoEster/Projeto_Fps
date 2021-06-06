using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Head Bob Data", menuName = "Data/Head Bob/Head Bob Data")]
public class HeadBobData : ScriptableObject
{
    [Header("Curves")]
    public AnimationCurve XCurve;
    public AnimationCurve YCurve;

    [Space, Header("Amplitude")]
    public float XAmplitude;
    public float YAmplitude;

    [Space, Header("Frequency")]
    public float xFrequency;
    public float yFrequency;

    [Space, Header("Run Multiplies")]
    public float runAmplitudeMultiplier;
    public float runFrequencyMultiplier;
    
    [Space, Header("Additional Frequency Multiplier")]
    public float RegularFrequencyMultiplier;
    public float MoveBackwardsFrequencyMultiplier;
    public float MoveSideFrequencyMultiplier;
}

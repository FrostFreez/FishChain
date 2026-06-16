using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FishSO", menuName = "Scriptable Objects/FishSO")]
public class FishSO : ScriptableObject, IComparable<FishSO>
{
    public string fishName;
    public int fishLevel;
    public float fishSpeed;
    public float mouthRadius;
    public float mouthOffset;
    public RuntimeAnimatorController animatorController;
    public float minimumTime = 0;
    public Vector2 colliderSize;

    public int CompareTo(FishSO other)
    {
        return (int)(minimumTime - other.minimumTime);
    }
}

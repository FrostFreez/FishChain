using UnityEngine;

[CreateAssetMenu(fileName = "FishSO", menuName = "Scriptable Objects/FishSO")]
public class FishSO : ScriptableObject
{
    public string fishName;
    public int fishLevel;
    public float fishSpeed;
    public float mouthRadius;
    public float mouthOffset;
    public RuntimeAnimatorController animatorController;
}

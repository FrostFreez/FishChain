using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Obstacle", order = 0)]
public class ObstacleSO : ScriptableObject, IComparable<ObstacleSO>
{
    public Vector2 initialVelocity;
    public Vector2 Acceleration;
    public SpawnArea[] spawnAreas;
    public float minimumTime = 0;

    public int CompareTo(ObstacleSO other)
    {
        return (int)(minimumTime - other.minimumTime);
    }
}

[Serializable]
public class SpawnArea
{
    public Vector2 Center;
    public Vector2 Size;
}

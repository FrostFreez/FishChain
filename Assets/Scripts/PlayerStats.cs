using UnityEngine;

public class PlayerStats : CoreComponent
{
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get { return instance; }
    }
    public float verticalSpeed = 5, horizontalSpeed = 0;
    public float totalTime = 0, totalDistance = 0;
    private void Awake()
    {
        instance = this;
    }
    public override void StartComponent()
    {
    }

    public override void UpdateComponent()
    {
        totalTime += Time.deltaTime;
        totalDistance += Time.deltaTime * horizontalSpeed;
    }
}

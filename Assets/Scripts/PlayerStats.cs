using UnityEngine;

public class PlayerStats : CoreComponent
{
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get { return instance; }
    }
    private FishLink link;
    public float verticalSpeed = 5, horizontalSpeed = 0;
    public float totalTime = 0, totalDistance = 0;
    private void Awake()
    {
        instance = this;
    }
    public override void StartComponent()
    {
        link = controller.Component<FishLink>();
    }

    public override void UpdateComponent()
    {
        totalTime += Time.deltaTime;
        totalDistance += Time.deltaTime * horizontalSpeed;
        if (link.fish == null)
        {
            horizontalSpeed -= 0.5f * Time.deltaTime * horizontalSpeed;
            if (horizontalSpeed < 0.1f)
            {
                horizontalSpeed = 0;
            }
        }
    }
}

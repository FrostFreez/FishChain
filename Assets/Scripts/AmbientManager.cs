using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    private static AmbientManager instance;
    public static AmbientManager Instance
    {
        get { return instance; }
    }

    public SpriteRenderer[] background;
    public TimeOfTheDay[] timeOfTheDay;
    public float boundLeft = -10;
    public float boundRight = 10;
    public float TOTDDuration = 20;
    private float TOTDCountdown = 0;
    private int TOTDStage = 0;

    private void Update()
    {
        TOTDCountdown += Time.deltaTime;
        if (TOTDCountdown > TOTDDuration)
        {
            TOTDCountdown -= TOTDDuration;
            TOTDStage = (TOTDStage + 1) % timeOfTheDay.Length;
        }
        foreach (var item in background)
        {
            item.transform.position += PlayerStats.Instance.horizontalSpeed * Time.deltaTime * Vector3.left;
            if (item.transform.position.x < boundLeft)
            {
                item.transform.position += Vector3.right * (boundRight - boundLeft);
                item.sprite = timeOfTheDay[TOTDStage].sprites[Random.Range(0, timeOfTheDay[TOTDStage].sprites.Length)];
            }
        }
    }
}

[System.Serializable]
public class TimeOfTheDay
{
    public Sprite[] sprites;
}
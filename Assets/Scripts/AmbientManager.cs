using UnityEngine;
using System;

public class AmbientManager : MonoBehaviour
{
    private static AmbientManager instance;
    public static AmbientManager Instance
    {
        get { return instance; }
    }

    public GameObject[] background;
    public TimeOfTheDay[] timeOfTheDay;
    public float boundLeft = -10;
    public float boundRight = 10;

    private void Update()
    {
        foreach(var item in background)
        {
            item.transform.position += PlayerStats.Instance.horizontalSpeed * Time.deltaTime * Vector3.left;
            if (item.transform.position.x < boundLeft)
            {
                item.transform.position += Vector3.right * (boundRight - boundLeft);
            }
        }
    }

}

[Serializable]
public class TimeOfTheDay
{
    public Sprite[] sprites;
}
using System;
using UnityEngine;


using Random = UnityEngine.Random;

public class FishManager : MonoBehaviour
{
    private static FishManager instance;
    public static FishManager Instance
    {
        get { return instance; }
    }

    public FishController fishPrefab;
    public FishSO[] fishSOs;
    public float boundLeft = -15;
    public float boundRight = 15;
    public float boundTop = 15;
    public float boundBottom = -15;
    public float fishCooldownStart = 6;
    public float fishCooldownAcceleration = .2f;
    public float fishCooldownMin = 1.5f;
    private float fishCountdown = 0;
    private float fishSpawningTime = 0;
    private int fishStage = 0;

    private void Update()
    {
        fishSpawningTime += Time.deltaTime;
        if (fishStage < fishSOs.Length - 1)
        {
            if (fishSpawningTime > fishSOs[fishStage + 1].minimumTime)
            {
                fishStage++;
            }
        }
        fishCountdown += Time.deltaTime;
        if (fishCountdown > fishCooldownStart)
        {
            fishCountdown -= fishCooldownStart;
            fishCooldownStart = (fishCooldownStart - fishCooldownMin) * fishCooldownAcceleration + fishCooldownMin;

            FishSO fishChosen = fishSOs[Random.Range(0, fishStage + 1)];
            FishController newFish = Instantiate(fishPrefab, new Vector2(12, Random.Range(-4f, 4f)), Quaternion.identity);
            newFish.GetComponentInChildren<FishHold>().fish = fishChosen;
        }
    }

    private void OnValidate()
    {
        Array.Sort(fishSOs);
    }
}

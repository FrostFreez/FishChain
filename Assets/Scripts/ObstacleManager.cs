using UnityEngine;
using System.Collections.Generic;
using System;

using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager instance;
    public static ObstacleManager Instance
    {
        get { return instance; }
    }

    public Rigidbody2D obstaclePrefab;
    public Dictionary<ObstacleSO, List<Rigidbody2D>> obstacles = new();
    public ObstacleSO[] obstacleSOs;
    public float boundLeft = -10;
    public float boundRight = 10;
    public float boundTop = 10;
    public float boundBottom = -10;
    public float obstacleCooldownStart = 6;
    public float obstacleCooldownAcceleration = .2f;
    public float obstacleCooldownMin = 1.5f;
    private float obstacleCountdown = 0;
    private float obstacleSpawningTime = 0;
    private int obstacleStage = 0;

    private void Update()
    {
        obstacleSpawningTime += Time.deltaTime;
        if (obstacleStage < obstacleSOs.Length - 1)
        {
            if (obstacleSpawningTime > obstacleSOs[obstacleStage + 1].minimumTime)
            {
                obstacleStage++;
            }
        }
        obstacleCountdown += Time.deltaTime;
        if (obstacleCountdown > obstacleCooldownStart)
        {
            obstacleCountdown -= obstacleCooldownStart;
            obstacleCooldownStart = (obstacleCooldownStart - obstacleCooldownMin) * obstacleCooldownAcceleration + obstacleCooldownMin;

            ObstacleSO obstacleChosen = obstacleSOs[Random.Range(0, obstacleStage + 1)];
            SpawnArea areaChosen = obstacleChosen.spawnAreas[Random.Range(0, obstacleChosen.spawnAreas.Length)];
            Rigidbody2D newObstacle = Instantiate(obstaclePrefab, new Vector2(
                Random.Range(areaChosen.Center.x - areaChosen.Size.x * 0.5f, areaChosen.Center.x + areaChosen.Size.x * 0.5f),
                Random.Range(areaChosen.Center.y - areaChosen.Size.y * 0.5f, areaChosen.Center.y + +areaChosen.Size.y * 0.5f)),
                Quaternion.identity);
            newObstacle.linearVelocity = obstacleChosen.initialVelocity;
            if (!obstacles.ContainsKey(obstacleChosen))
            {
                obstacles[obstacleChosen] = new();
            }
            obstacles[obstacleChosen].Add(newObstacle);
        }
        foreach (var item in obstacles)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                item.Value[i].linearVelocity += item.Key.Acceleration * Time.deltaTime;
                if (item.Value[i].transform.position.x < boundLeft || item.Value[i].transform.position.x > boundRight ||
                    item.Value[i].transform.position.y < boundBottom || item.Value[i].transform.position.y > boundTop)
                {
                    Destroy(item.Value[i].gameObject);
                    item.Value.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    private void OnValidate()
    {
        Array.Sort(obstacleSOs);
    }
}

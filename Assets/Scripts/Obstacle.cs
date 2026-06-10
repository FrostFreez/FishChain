using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        transform.position += PlayerStats.Instance.horizontalSpeed * Time.deltaTime * Vector3.left;
    }
}

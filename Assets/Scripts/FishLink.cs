using System;
using Unity.VisualScripting;
using UnityEngine;

public class FishLink : CoreComponent
{
    public PlayerInput pi;
    public FishController fish;
    public GameObject circle;
    public float timeThrowing = 0;
    public float catchRadius = 3;
    public float catchSpeed = 3;
    public float initialOffset = 0.9f;

    private bool firstFishCaught = false;

    public override void StartComponent()
    {
        Collider2D[] fishes = Physics2D.OverlapCircleAll(transform.position + +initialOffset * Vector3.right,
            catchRadius * 2, LayerMask.GetMask("Fish"));
        fish = fishes[0].GetComponent<FishController>();

        circle.transform.localScale = 2 * catchRadius * Vector2.one;
        circle.SetActive(false);
    }

    public override void UpdateComponent()
    {
        if (!firstFishCaught)
        {
            Caught();
            firstFishCaught = true;
        }
        if (fish != null)
        {
            fish.transform.position = new Vector2(fish.transform.position.x, transform.position.y);
            if (pi.throwPressed && fish.stateMachine.ActiveState().GetType() == typeof(LinkState))
            {
                timeThrowing = 0;
                fish.stateMachine.ChangeState(fish.State<FreeState>());
                circle.SetActive(true);
            }
        }
        if (pi.throwHeld)
        {
            Time.timeScale = 0.8f;
            timeThrowing += Time.deltaTime;
        }
        else
        {
            Time.timeScale = 1f;
        }
        circle.transform.position = transform.position + (catchSpeed * timeThrowing + initialOffset) * Vector3.right;
        if (pi.throwReleased)
        {
            Catch();
            circle.SetActive(false);
        }
    }
    private void Catch()
    {
        Collider2D[] fishes = Physics2D.OverlapCircleAll(
            transform.position + (catchSpeed * timeThrowing + initialOffset)
            * Vector3.right, catchRadius, LayerMask.GetMask("Fish"));
        if (fishes.Length > 0)
        {
            Debug.Log("Called: " + fishes[0].GetComponent<FishController>().stateMachine.ActiveState().GetType());
            if (fishes[0].GetComponent<FishController>().stateMachine.ActiveState().GetType() == typeof(TameState))
            {
                fish = fishes[0].GetComponent<FishController>();
                Caught();
            }
        }
    }
    private void Caught()
    {
        timeThrowing = 0;
        PlayerStats.Instance.horizontalSpeed = fish.Component<FishHold>().fish.fishSpeed;
        PlayerStats.Instance.maxSpeed = fish.Component<FishHold>().fish.fishSpeed;
        fish.stateMachine.ChangeState(fish.State<LinkState>());
        Vector2 newPos = Vector2.right * initialOffset;
        newPos += fish.GetComponent<CapsuleCollider2D>().bounds.size * 0.5f * Vector2.right;
        fish.transform.position = (Vector2)transform.position + newPos;
        catchSpeed = fish.Component<FishHold>().fish.fishSpeed * 2;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (catchSpeed * timeThrowing + initialOffset) * Vector3.right, catchRadius);
    }
}

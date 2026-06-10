using UnityEngine;

public class FishLink : CoreComponent
{
    public PlayerInput pi;
    public FishController fish;
    public float timeWithoutFish = 0;
    public float catchRadius = 3;
    public float catchSpeed = 3;
    public float initialOffset = 0.9f;

    public override void StartComponent()
    {
        pi = controller.Component<PlayerInput>();
    }

    public override void UpdateComponent()
    {
        if (fish != null)
        {
            if (pi.throwPressed)
            {

            }
            fish.transform.position = new Vector2(fish.transform.position.x, transform.position.y);
        }
        else
        {
            timeWithoutFish += Time.deltaTime;
            Catch();
        }
    }
    private void Catch()
    {
        Collider2D[] fishes = Physics2D.OverlapCircleAll(transform.position + (catchSpeed * timeWithoutFish + initialOffset) * Vector3.right,
            catchRadius, LayerMask.GetMask("Fish"));
        if (fishes.Length > 0)
        {
            timeWithoutFish = 0;
            fish = fishes[0].GetComponent<FishController>();
            PlayerStats.Instance.horizontalSpeed = fish.Component<FishHold>().fish.fishSpeed;
            fish.stateMachine.ChangeState(fish.State<LinkState>());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (catchSpeed * timeWithoutFish + initialOffset) * Vector3.right, catchRadius);
    }
}

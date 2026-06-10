using UnityEngine;

public class EatFish : CoreComponent
{
    public FishSO fish;
    public override void StartComponent()
    {
        Debug.Log(controller);
        fish = controller.Component<FishHold>().fish;
    }

    public override void UpdateComponent()
    {
        if (controller.stateMachine.ActiveState().GetType() == typeof(SwimState))
        {
            Eat();
        }
        else if(controller.stateMachine.ActiveState().GetType() == typeof(LinkState))
        {
            Die();
        }
    }
    private void Eat()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            (Vector2)transform.position + Vector2.left * fish.mouthOffset, fish.mouthRadius, LayerMask.GetMask("Fish"));
        if (colliders.Length > 1)
        {
            FishController other;
            if (colliders[0].gameObject == controller.gameObject)
            {
                other = colliders[1].GetComponent<FishController>();
            }
            else
            {
                other = colliders[0].GetComponent<FishController>();
            }
            if (other == null) { return; }

            FishSO otherFish = other.Component<FishHold>().fish;

            if (other.stateMachine.ActiveState().GetType() == typeof(FreeState))
            {
                if (otherFish.fishLevel < fish.fishLevel)
                {
                    controller.stateMachine.ChangeState(controller.State<TameState>());
                    Destroy(other.gameObject);
                }
                else if (otherFish.fishLevel == fish.fishLevel)
                {
                    Destroy(other.gameObject);
                    Destroy(controller.gameObject);
                }
                else
                {
                    Destroy(controller.gameObject);
                }
            }
        }
    }
    private void Die()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            (Vector2)transform.position + Vector2.right * fish.mouthOffset, fish.mouthRadius, LayerMask.GetMask("Obstacle"));
        if (colliders.Length > 0)
        {
            Debug.Log("Die");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position + Vector3.left * fish.mouthOffset, fish.mouthRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.right * fish.mouthOffset, fish.mouthRadius);
    }
}

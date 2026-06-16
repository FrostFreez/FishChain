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
            Eat(-1);
        }
        else if (controller.stateMachine.ActiveState().GetType() == typeof(LinkState))
        {
            Die();
            Eat(1);
        }
    }
    private void Eat(int flip)
    {
        if (controller.stateMachine.ActiveState().GetType() != typeof(DieState))
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                (Vector2)transform.position + fish.mouthOffset * flip * Vector2.right, fish.mouthRadius, LayerMask.GetMask("Fish"));
            if (colliders.Length > 1)
            {
                FishController otherFish;
                if (colliders[0].gameObject == controller.gameObject)
                {
                    otherFish = colliders[1].GetComponent<FishController>();
                }
                else
                {
                    otherFish = colliders[0].GetComponent<FishController>();
                }
                if (otherFish == null) { return; }

                FishSO otherFishSO = otherFish.Component<FishHold>().fish;

                if (otherFish.stateMachine.ActiveState().GetType() != typeof(DieState))
                {
                    if (otherFishSO.fishLevel + 1 == fish.fishLevel)
                    {
                        if (controller.stateMachine.ActiveState().GetType() == typeof(SwimState))
                        {
                            Debug.Log("Called1");
                            controller.stateMachine.ChangeState(controller.State<TameState>());
                        }
                    }
                    else if (otherFishSO.fishLevel - 1 == fish.fishLevel)
                    {
                        Debug.Log("Called2: " + otherFish.stateMachine.ActiveState().GetType());
                        if (otherFish.stateMachine.ActiveState().GetType() == typeof(SwimState))
                        {
                            otherFish.stateMachine.ChangeState(otherFish.State<TameState>());
                        }
                    }
                    if (otherFishSO.fishLevel < fish.fishLevel)
                    {
                        otherFish.stateMachine.ChangeState(otherFish.State<DieState>());
                        controller.anim.SetTrigger("Eat");
                    }
                    else if (otherFishSO.fishLevel == fish.fishLevel)
                    {
                        Debug.Log("Called3");
                        otherFish.stateMachine.ChangeState(otherFish.State<DieState>());
                        controller.stateMachine.ChangeState(controller.State<DieState>());
                        GameManager.Instance.Lose();
                    }
                    else
                    {
                        controller.stateMachine.ChangeState(controller.State<DieState>());
                        otherFish.anim.SetTrigger("Eat");
                    }
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
            controller.stateMachine.ChangeState(controller.State<DieState>());
            GameManager.Instance.Lose();
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

using UnityEngine;

public class EatFish : CoreComponent
{
    FishSO fish;
    public override void StartComponent()
    {
        Debug.Log(controller);
        fish = controller.Component<FishHold>().fish;
    }

    public override void UpdateComponent()
    {
        if (!(controller.stateMachine.ActiveState().GetType() == typeof(SwimState)))
        {
            return;
        }
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
            if (other.stateMachine.ActiveState().GetType() == typeof(FreeState))
            {
                controller.stateMachine.ChangeState(controller.State<TameState>());
                Destroy(other.gameObject);
            }
        }
    }
}

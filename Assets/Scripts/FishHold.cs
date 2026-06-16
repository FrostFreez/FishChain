using UnityEngine;

public class FishHold : CoreComponent
{
    public FishSO fish;
    public override void StartComponent()
    {
        controller.anim.runtimeAnimatorController = fish.animatorController;
        controller.col.size = fish.colliderSize;
    }

    public override void UpdateComponent()
    {
    }
}

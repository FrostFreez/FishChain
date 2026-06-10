using UnityEngine;

public class FishController : EntityController
{
    protected override void Awake()
    {
        base.Awake();
        states.Add(new SwimState(this, stateMachine, "Swim"));
        states.Add(new TameState(this, stateMachine, "fed"));
        states.Add(new LinkState(this, stateMachine, "Swim"));
        states.Add(new FreeState(this, stateMachine, "Swim"));
    }
}

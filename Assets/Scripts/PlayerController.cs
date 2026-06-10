using UnityEngine;

public class PlayerController : EntityController
{
    protected override void Awake()
    {
        base.Awake();
        states.Add(new MoveState(this, stateMachine, "Move"));
    }
}

using UnityEngine;

public class SwimState : BaseState
{
    private Rigidbody2D rb;
    private FishSO fish;
    public SwimState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
        rb = controller.rb;
        fish = controller.Component<FishHold>().fish;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocityX = -fish.fishSpeed - PlayerStats.Instance.horizontalSpeed;
    }

    
}

public class LinkState : BaseState
{
    public LinkState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        controller.sr.flipX = true;
        controller.rb.linearVelocity = Vector2.zero;
    }
}

public class FreeState : BaseState
{
    private Rigidbody2D rb;
    private FishSO fish;
    public FreeState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
        rb = controller.rb;
        fish = controller.Component<FishHold>().fish;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocityX = fish.fishSpeed;
    }
}
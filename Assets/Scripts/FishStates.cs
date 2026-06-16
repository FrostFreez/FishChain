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
    public override void Enter()
    {
        base.Enter();
        controller.sr.flipX = true;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (fish == null)
        {
            fish = controller.Component<FishHold>().fish;
        }
        rb.linearVelocityX = -fish.fishSpeed - PlayerStats.Instance.horizontalSpeed;
    }


}

public class TameState : BaseState
{
    private Rigidbody2D rb;
    public TameState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
        rb = controller.rb;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocityX = -PlayerStats.Instance.horizontalSpeed;
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
        controller.rb.linearVelocity = Vector2.zero;
        controller.sr.flipX = false;
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
    public override void Enter()
    {
        base.Enter();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (fish == null)
        {
            fish = controller.Component<FishHold>().fish;
        }
        rb.linearVelocityX = fish.fishSpeed * 2;
    }
}

public class DieState : BaseState
{
    AutoDestroy destroy;
    public float timeToDie = .2f;
    public DieState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
        destroy = controller.Component<AutoDestroy>();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        timeToDie -= Time.deltaTime;
        if (timeToDie < 0)
        {
            destroy.DestroySelf();
        }
    }
}
using UnityEngine;

public class MoveState : BaseState
{
    public PlayerInput pi;
    public Rigidbody2D rb;
    public Vector2 direction;
    public MoveState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
        pi = controller.Component<PlayerInput>();
        rb = controller.rb;
        direction = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        direction.y = pi.move.y * PlayerStats.Instance.verticalSpeed;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocity = direction;
    }
}

public class CatchState : BaseState
{
    public PlayerInput pi;
    public Rigidbody2D rb;
    public CatchState(EntityController controller, StateMachine stateMachine, string animName) : base(controller, stateMachine, animName)
    {
        pi = controller.Component<PlayerInput>();
        rb = controller.rb;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

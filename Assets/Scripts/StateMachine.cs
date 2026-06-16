using UnityEngine;

public class StateMachine
{
    private BaseState activeState;
    public void Initialize(BaseState startState)
    {
        activeState = startState;
        activeState.Enter();
    }
    public BaseState ActiveState()
    {
        return activeState;
    }
    public void ChangeState(BaseState newState)
    {
        activeState.Exit();
        activeState = newState;
        activeState.Enter();
    }
}

[System.Serializable]
public abstract class BaseState
{
    protected EntityController controller;
    protected StateMachine stateMachine;
    protected string animName;
    protected float enterTime = 0;

    public BaseState(EntityController controller, StateMachine stateMachine, string animName)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter() { enterTime = Time.time; DoChecks(); controller.anim.SetBool(animName, true); }
    public virtual void DoChecks() { }
    public virtual void Update() { DoChecks(); }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { controller.anim.SetBool(animName, false); }
}
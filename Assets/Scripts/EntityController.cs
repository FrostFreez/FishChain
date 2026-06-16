using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EntityController : MonoBehaviour
{
    [SerializeField] protected List<BaseState> states = new();
    private CoreComponent[] components = { };
    public StateMachine stateMachine;
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public CapsuleCollider2D col;
    protected virtual void Awake()
    {
        components = GetComponentsInChildren<CoreComponent>();
        stateMachine = new();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        stateMachine.Initialize(states[0]);
        foreach (CoreComponent c in components)
        {
            c.SetUp(this);
        }
        foreach (CoreComponent c in components)
        {
            c.StartComponent();
        }
    }

    void Update()
    {
        foreach (CoreComponent c in components)
        {
            c.UpdateComponent();
        }
        stateMachine.ActiveState().Update();
    }
    private void FixedUpdate()
    {
        stateMachine.ActiveState().FixedUpdate();
    }
    public T Component<T>() where T : CoreComponent
    {
        foreach (CoreComponent c in components)
        {
            if (c is T ret)
            {
                return ret;
            }
        }
        Debug.LogWarning("No " + typeof(T) + " component was found!");
        return null;
    }
    public T State<T>() where T : BaseState
    {
        foreach (BaseState c in states)
        {
            if (c is T ret)
            {
                return ret;
            }
        }
        Debug.LogWarning("No " + typeof(T) + " component was found!");
        return null;
    }
}

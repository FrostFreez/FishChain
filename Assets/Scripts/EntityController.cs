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
    protected virtual void Awake()
    {
        components = GetComponentsInChildren<CoreComponent>();
        stateMachine = new();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        stateMachine.Initialize(states[0]);
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
        return (T)components.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
    }
    public T State<T>() where T : BaseState
    {
        return (T)states.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
    }
}

using UnityEngine;

public abstract class CoreComponent : MonoBehaviour
{
    protected EntityController controller;
    public void SetUp(EntityController controller)
    {
        this.controller = controller;
    }
    public abstract void StartComponent();
    public abstract void UpdateComponent();
}

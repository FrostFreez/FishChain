public class AutoDestroy : CoreComponent
{
    public static float boundLeft = -15;
    public static float boundRight = 15;
    public static float boundTop = 15;
    public static float boundBottom = -15;
    public override void StartComponent()
    {
    }

    public override void UpdateComponent()
    {
        if (transform.position.x < boundLeft || transform.position.x > boundRight ||
            transform.position.y < boundBottom || transform.position.y > boundTop)
        {
            Destroy(controller.gameObject);
        }
    }
    public void DestroySelf()
    {
        if (controller != null)
        {
            Destroy(controller.gameObject);
        }
    }
}

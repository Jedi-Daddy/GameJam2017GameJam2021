using UnityEngine;

public class HPController : MonoBehaviour
{
    public Transform Bar;

    private void OnEnable()
    {
        EventDispatcher.OnHpUpdated += OnHpUpdated;
    }

    private void OnDisable()
    {
        EventDispatcher.OnHpUpdated -= OnHpUpdated;
    }

    public void OnHpUpdated(object sender, IntEventArgs args)
    {
        Bar.localScale = new Vector3(args.Value / 100f, Bar.localScale.y, Bar.localScale.z);
    }
}

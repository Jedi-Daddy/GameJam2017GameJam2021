using UnityEngine;

public class SunRotator : MonoBehaviour
{
    private Vector3? lookPoint;

    private void OnEnable()
    {
        EventDispatcher.OnSunDirectionUpdated += OnSunDirectionUpdated;
    }

    private void OnDisable()
    {
        EventDispatcher.OnSunDirectionUpdated -= OnSunDirectionUpdated;
    }

    public void OnSunDirectionUpdated(object sender, PositionEventArgs args)
    {
        //lookPoint = args.Position;
        transform.LookAt(args.Position);
    }

    private void Update()
    {
        if (!lookPoint.HasValue)
            return;

        var lookRotation = Quaternion.LookRotation(lookPoint.Value);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

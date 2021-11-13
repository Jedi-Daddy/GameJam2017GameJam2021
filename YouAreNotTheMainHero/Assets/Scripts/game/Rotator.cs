using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float period = 5f;

    private Quaternion rotation;
    private float time;

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
        time = 0f;

        //transform.LookAt(args.Position);
        Vector3 lookDirection = args.Position - transform.position;
        rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }

    private void Update()
    {
        if (time > period)
            return;

        time += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, time / period);
    }
}

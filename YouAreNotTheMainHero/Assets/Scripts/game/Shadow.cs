using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Level Level;
    public Transform Obelisk;
    public float DamageDelay;
    public float DamageInterval;
    public int Damage;

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
        OnPointSelected(args.Position);
    }

    public void OnPointSelected(Vector3 position)
    {
        var lookPosition = new Vector3(-position.x, position.y, -position.z);
        transform.LookAt(position);
    }
}

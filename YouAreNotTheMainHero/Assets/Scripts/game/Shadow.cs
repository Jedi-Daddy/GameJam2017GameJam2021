using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Level Level;
    public Transform Obelisk;
    public float DamageDelay;
    public float DamageInterval;
    public int Damage;

    public void OnPointSelected(int idx)
    {
        var position = Level.SpawnPoints[idx].position;
        var lookPosition = new Vector3(-position.x, position.y, -position.z);
        transform.LookAt(position);
    }
}

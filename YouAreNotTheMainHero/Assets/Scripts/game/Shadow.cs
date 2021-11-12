using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Level Level;
    public Transform Obelisk;

    public void OnPointSelected(int idx)
    {
        var position = Level.SpawnPoints[idx].position;
        transform.LookAt(position);
    }
}

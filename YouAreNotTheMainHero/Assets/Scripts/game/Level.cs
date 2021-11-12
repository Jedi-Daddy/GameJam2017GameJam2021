using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public float Delay;
    public Enemy EnemyPrefab;
    public Transform Target;

    private Vector3 NextSpawnPoint => SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;

    private float time;

    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            Spawn();
            time = Delay;
        }
    }

    private void Spawn()
    {
        var enemy = Instantiate(EnemyPrefab);
        enemy.transform.position = NextSpawnPoint;
        enemy.transform.LookAt(Target);
    }
}

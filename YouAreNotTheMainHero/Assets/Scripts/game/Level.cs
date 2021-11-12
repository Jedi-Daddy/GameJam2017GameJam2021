using UnityEngine;

public class Level : MonoBehaviour
{
    public Enemy[] Enemies;
    public Transform[] SpawnPoints;
    public float Delay;
    public Transform Target;
    public bool AutoSpawn;

    private Enemy NextEnemy => Enemies[Random.Range(0, Enemies.Length)];
    private Vector3 NextSpawnPoint => SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;

    private float time;

    private void Update()
    {
        if (!AutoSpawn)
            return;

        time -= Time.deltaTime;

        if (time < 0)
        {
            Spawn();
            time = Delay;
        }
    }

    public void Spawn()
    {
        Spawn(NextEnemy);
    }

    public void Spawn(Enemy prefab)
    {
        var enemy = Instantiate(prefab);
        enemy.transform.position = NextSpawnPoint;
        enemy.transform.LookAt(Target);
    }
}

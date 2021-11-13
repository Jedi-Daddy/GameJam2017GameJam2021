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

    private void OnEnable()
    {
        EventDispatcher.OnSunUpdated += OnSunUpdated;
        EventDispatcher.OnStartPosition += OnStartPosition;
    }

    private void OnDisable()
    {
        EventDispatcher.OnSunUpdated -= OnSunUpdated;
        EventDispatcher.OnStartPosition -= OnStartPosition;
    }

    public void OnStartPosition(object sender, IntEventArgs args)
    {
        EventDispatcher.OnStarDirectiontPosition?.Invoke(this, new PositionEventArgs(SpawnPoints[args.Idx].position));
    }

    public void OnSunUpdated(object sender, IntEventArgs args)
    {
        EventDispatcher.OnSunDirectionUpdated?.Invoke(this, new PositionEventArgs(SpawnPoints[args.Idx].position));
    }

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
        var lookTarget = new Vector3(
            Target.position.x,
            enemy.transform.position.y,
            Target.position.z);
        enemy.transform.LookAt(lookTarget);
    }
}

using System;
using UnityEngine;

[Serializable]
public class Script
{
    public Enemy Enemy;
    public float SecondsFromStart;
    public int Path;
}

public class Level : MonoBehaviour
{
    public Enemy[] Enemies;
    public Transform[] SpawnPoints;
    public Script[] Scripts;
    public float Delay;
    public Transform Target;
    public bool AutoSpawn;

    private Enemy NextEnemy => Enemies[UnityEngine.Random.Range(0, Enemies.Length)];
    private Vector3 NextSpawnPoint => SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)].position;

    private float time;
    private int idx;

    private void OnEnable()
    {
        EventDispatcher.OnSunStarted += OnSunStarted;
        EventDispatcher.OnSunUpdated += OnSunUpdated;
        EventDispatcher.OnStartPosition += OnStartPosition;
    }

    private void OnDisable()
    {
        EventDispatcher.OnSunStarted -= OnSunStarted;
        EventDispatcher.OnSunUpdated -= OnSunUpdated;
        EventDispatcher.OnStartPosition -= OnStartPosition;
    }

    public void OnSunStarted(object sender, IntEventArgs args)
    {
        EventDispatcher.OnSunDirectionUpdated?.Invoke(this, new PositionEventArgs(SpawnPoints[args.Value].position, true));
    }

    public void OnSunUpdated(object sender, IntEventArgs args)
    {
        EventDispatcher.OnSunDirectionUpdated?.Invoke(this, new PositionEventArgs(SpawnPoints[args.Value].position));
    }

    public void OnStartPosition(object sender, IntEventArgs args)
    {
        EventDispatcher.OnStarDirectiontPosition?.Invoke(this, new PositionEventArgs(SpawnPoints[args.Value].position));
    }

    private void Update()
    {
        if (AutoSpawn)
        {
            time -= Time.deltaTime;

            if (time < 0)
            {
                Spawn();
                time = Delay;
            }
        }
        else if (idx < Scripts.Length)
        {
            time += Time.deltaTime;

            if (time > Scripts[idx].SecondsFromStart)
            {
                var script = Scripts[idx];
                Spawn(script.Enemy, SpawnPoints[script.Path - 1].position);
                idx++;
            }
        }
        else
        {
            time = 0;
            AutoSpawn = true;
        }
    }

    public void Spawn()
    {
        Spawn(NextEnemy);
    }

    public void Spawn(Enemy prefab)
    {
        Spawn(prefab, NextSpawnPoint);
    }

    public void Spawn(Enemy prefab, Vector3 position)
    {
        var enemy = Instantiate(prefab);
        enemy.transform.position = position;
        var lookTarget = new Vector3(
            Target.position.x,
            enemy.transform.position.y,
            Target.position.z);
        enemy.transform.LookAt(lookTarget);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Level Level;
    public Transform Obelisk;
    public float DamageDelay;
    public float DamageInterval;
    public int Damage;

    public float RotationAmount = 2f;
    public int TicksPerSecond = 60;

    public float AngleMinimum = 0;

    public Dictionary<int, int> PathRotation = new Dictionary<int, int>
        {
            { 0, 178 },
            { 1, 224 },
            { 2, 268 },
            { 3, 316 },
            { 4, 0 },
            { 5, 44 },
            { 6, 90 },
        };

    private void OnEnable()
    {
        EventDispatcher.OnSunUpdated += OnSunUpdated;
        EventDispatcher.OnStarDirectiontPosition += OnStarDirectiontPosition;
    }

    private void OnDisable()
    {
        EventDispatcher.OnSunUpdated -= OnSunUpdated;
        EventDispatcher.OnStarDirectiontPosition -= OnStarDirectiontPosition;
    }

    public void OnStarDirectiontPosition(object sender, PositionEventArgs args)
    {
        transform.LookAt(args.Position);
    }

    public void OnSunUpdated(object sender, IntEventArgs args)
    {
        var neededRoration = PathRotation[args.Idx];
        StartCoroutine(Rotate(neededRoration, args.IsClockwise));
    }

    public IEnumerator Rotate(int neededRoration, bool isClockwice)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / TicksPerSecond);

        while (NeedRotation(neededRoration))
        {
            if (isClockwice)
                transform.Rotate(Vector3.up * RotationAmount);
            else
                transform.Rotate(Vector3.down * RotationAmount);
            yield return wait;
        }
    }

    private bool NeedRotation(int dest)
    {
        var cur = (int)transform.localEulerAngles.y;

        var min = dest - AngleMinimum;
        if (dest == 0 && AngleMinimum != 0)
            min = 360 - AngleMinimum;

        var max = dest + AngleMinimum;

        if (cur == dest || (cur >= min && cur <= max))
            return false;
        return true;
    }
}

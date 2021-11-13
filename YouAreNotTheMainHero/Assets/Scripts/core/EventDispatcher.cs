using System;
using UnityEngine;

public static class EventDispatcher
{
    public static EventHandler<IntEventArgs> OnSunUpdated;
    public static EventHandler<PositionEventArgs> OnSunDirectionUpdated;
    public static EventHandler<IntEventArgs> OnStartPosition;
    public static EventHandler<PositionEventArgs> OnStarDirectiontPosition;
}


public class PositionEventArgs : EventArgs
{
    public Vector3 Position { get; set; }

    public PositionEventArgs(Vector3 vector)
    {
        Position = vector;
    }
}

public class IntEventArgs : EventArgs
{
    public int Idx { get; set; }
    public bool IsClockwise { get; set; }

    public IntEventArgs(int idx, bool isClockwise = true)
    {
        Idx = idx;
        IsClockwise = isClockwise;
    }
}

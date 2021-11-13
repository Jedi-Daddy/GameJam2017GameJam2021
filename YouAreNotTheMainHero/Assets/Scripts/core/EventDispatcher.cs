using System;
using UnityEngine;

public static class EventDispatcher
{
    public static EventHandler<IntEventArgs> OnSunStarted;
    public static EventHandler<IntEventArgs> OnSunUpdated;
    public static EventHandler<PositionEventArgs> OnSunDirectionUpdated;
    public static EventHandler<IntEventArgs> OnStartPosition;
    public static EventHandler<PositionEventArgs> OnStarDirectiontPosition;
}


public class PositionEventArgs : EventArgs
{
    public Vector3 Position { get; set; }
    public bool Immediately { get; set; }

    public PositionEventArgs(Vector3 vector, bool immediately = false)
    {
        Position = vector;
        Immediately = immediately;
    }
}

public class IntEventArgs : EventArgs
{
    public int Idx { get; set; }
    public bool IsClockwise { get; set; }

    public IntEventArgs(int idx)
    {
        Idx = idx;
    }
}

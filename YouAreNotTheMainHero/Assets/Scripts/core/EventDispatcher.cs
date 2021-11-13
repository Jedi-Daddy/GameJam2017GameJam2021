using System;
using UnityEngine;

public static class EventDispatcher
{
    public static EventHandler<IntEventArgs> OnSunStarted;
    public static EventHandler<IntEventArgs> OnSunUpdated;
    public static EventHandler<PositionEventArgs> OnSunDirectionUpdated;
    public static EventHandler<IntEventArgs> OnStartPosition;
    public static EventHandler<PositionEventArgs> OnStarDirectiontPosition;

    public static EventHandler<IntEventArgs> OnHpUpdated;
    public static EventHandler<EventArgs> OnGameOver;
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
    public int Value { get; set; }

    public IntEventArgs(int value)
    {
        Value = value;
    }
}

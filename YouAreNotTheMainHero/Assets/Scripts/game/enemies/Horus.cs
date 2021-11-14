using System;
using System.Collections;
using UnityEngine;

public class Horus : Enemy
{
    public float FreezeTime = 3;

    private Coroutine freeze;

    protected override void OnShadowEnter()
    {
        base.OnShadowEnter();

        freeze = StartCoroutine(FreezeSun());
    }

    private IEnumerator FreezeSun()
    {
        DispatchStartEvent();

        yield return new WaitForSeconds(FreezeTime);

        DispatchStopEvent();
    }

    protected override void OnDied()
    {
        base.OnDied();

        if (freeze != null)
            StopCoroutine(freeze);

        DispatchStopEvent();
    }

    private void DispatchStartEvent()
    {
        EventDispatcher.OnSunLock?.Invoke(this, new EventArgs());
    }

    private void DispatchStopEvent()
    {
        EventDispatcher.OnSunUnlock?.Invoke(this, new EventArgs());
    }
}

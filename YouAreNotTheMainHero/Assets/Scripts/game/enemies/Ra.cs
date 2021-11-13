using System;
using UnityEngine;

public class Ra : Enemy
{
    public int RisePercentage = 200;
    public float IncSpeed = 0.1f;
    public float DecSpeed = 3f;

    private Vector3 startScale;
    private int direction;
    private float speed;

    protected override bool IsDying => transform.localScale.sqrMagnitude < 4500f;

    protected override void Start()
    {
        base.Start();

        HP = int.MaxValue;

        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        DamageStoped();
    }

    private void OnDisable()
    {
        DamageStarted();
    }

    protected override void Update()
    {
        base.Update();

        if (IsDying)
            return;

        transform.localScale = Vector3.Lerp(transform.localScale, startScale + (direction * startScale * RisePercentage / 100f), Time.deltaTime * speed);

        if (IsDying)
            StartCoroutine(Die());
    }

    protected override void DamageStarted()
    {
        base.DamageStarted();

        direction = -1;
        speed = DecSpeed;
    }

    protected override void DamageStoped()
    {
        base.DamageStoped();

        direction = 1;
        speed = IncSpeed;
    }
}

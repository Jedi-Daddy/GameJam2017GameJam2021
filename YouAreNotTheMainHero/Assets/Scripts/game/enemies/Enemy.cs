using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public int HP = 20;
    public int Damage = 10;

    private bool isDying;
    protected virtual bool IsDying => isDying;

    private Animation[] animations;

    protected virtual void OnShadowEnter() { }

    protected virtual void OnDamageStarted() { }

    protected virtual void OnDamageStoped() { }

    protected virtual void OnApplyDamag(int damage)
    {
        Debug.Log($"Enemy apply damage {name} {damage}");
        HP -= damage;
        TryPlayAnimation("Damage");
    }

    protected virtual void OnDie() { }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
        animations = GetComponentsInChildren<Animation>();
    }

    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        if (IsDying)
            return;

        var amountToMove = Speed * Time.deltaTime;
        transform.Translate(Vector3.forward * amountToMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obelisk")
        {
            OnDied();
        } 
        else if (other.gameObject.tag == "Shadow")
        {
            var shadow = other.gameObject.GetComponent<Shadow>();
            StartCoroutine(ShadowEnter(shadow));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        OnDamageStoped();
    }

    protected IEnumerator ShadowEnter(Shadow shadow)
    {
        OnShadowEnter();

        yield return new WaitForSeconds(shadow.DamageDelay);
        Debug.Log($"Enemy start damage {name}");

        OnDamageStarted();

        while (HP > 0)
        {
            OnApplyDamag(shadow.Damage);
            yield return new WaitForSeconds(shadow.DamageInterval);
        }

        StartCoroutine(Die());
        Debug.Log($"Enemy dead {name}");
    }

    protected IEnumerator Die()
    {
        OnDie();

        isDying = true;
        GetComponent<Collider>().enabled = false;

        if (TryPlayAnimation("Death"))
            yield return new WaitForSeconds(1.5f);

        OnDied();
        EventDispatcher.OnEnemyDiedByShadow.Invoke(this, new EventArgs());
    }

    private bool TryPlayAnimation(string name)
    {
        foreach(var animation in animations)
        {
            var clip = animation.GetClip(name);
            if (clip != null && animation.Play(name))
                return true;
        }

        return false;
    }
 }
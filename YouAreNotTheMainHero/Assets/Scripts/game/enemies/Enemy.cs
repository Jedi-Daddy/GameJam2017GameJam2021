using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public int HP = 20;
    public int Damage = 10;

    private bool isDying;
    protected virtual bool IsDying => isDying;

    private Animation animation;

    protected virtual void DamageStarted()
    {
        //TryPlayAnimation("Damage");
    }

    protected virtual void DamageStoped() { }

    protected virtual void ApplyDamag(int damage)
    {
        Debug.Log($"Enemy apply damage {name} {damage}");
        HP -= damage;
    }

    protected virtual void Start()
    {
        animation = GetComponent<Animation>();
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
            Destroy(gameObject);
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
        DamageStoped();
    }

    private IEnumerator ShadowEnter(Shadow shadow)
    {
        yield return new WaitForSeconds(shadow.DamageDelay);
        Debug.Log($"Enemy start damage {name}");

        DamageStarted();

        while (HP > 0)
        {
            ApplyDamag(shadow.Damage);
            yield return new WaitForSeconds(shadow.DamageInterval);
        }

        StartCoroutine(Die());
        Debug.Log($"Enemy dead {name}");
    }

    protected IEnumerator Die()
    {
        isDying = true;

        if (TryPlayAnimation("Death"))
        {
            GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(1.5f);
        }

        Destroy(gameObject);
    }

    private bool TryPlayAnimation(string name)
    {
        var clip = animation.GetClip(name);
        return clip != null && animation.Play(name);
    }
 }
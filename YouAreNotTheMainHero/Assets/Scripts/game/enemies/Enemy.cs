using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public int HP = 20;
    public int Damage = 10;

    private bool isDying;
    protected virtual bool IsDying => isDying;

    protected virtual void DamageStarted() { }

    protected virtual void DamageStoped() { }

    protected virtual void ApplyDamag(int damage)
    {
        HP -= damage;
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
            StartCoroutine(ApplyDamage(shadow.DamageDelay, shadow.DamageInterval, shadow.Damage));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        DamageStoped();
    }

    private IEnumerator ApplyDamage(float delay, float interval, int damage)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"Enemy start damage {name}");

        DamageStarted();

        while (HP > 0)
        {
            ApplyDamag(damage);
            Debug.Log($"Enemy apply damage {name} {damage}");
            yield return new WaitForSeconds(interval);
        }

        StartCoroutine(Die());
        Debug.Log($"Enemy dead {name}");
    }

    public IEnumerator Die()
    {
        isDying = true;

        var animation = GetComponent<Animation>();
        var clip = animation.GetClip("Death");
        if (clip != null && animation.Play("Death"))
        {
            GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(1.5f);
        }

        Destroy(gameObject);
    }
 }
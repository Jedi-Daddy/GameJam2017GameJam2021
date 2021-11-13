using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public int HP = 20;
    public int Damage = 10;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
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
    }

    private IEnumerator ApplyDamage(float delay, float interval, int damage)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"Enemy start damage {name}");

        while (HP > 0)
        {
            HP -= damage;
            Debug.Log($"Enemy apply damage {name} {damage}");
            yield return new WaitForSeconds(interval);
        }

        StartCoroutine(Die());
        Debug.Log($"Enemy dead {name}");
    }

    private IEnumerator Die()
    {
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
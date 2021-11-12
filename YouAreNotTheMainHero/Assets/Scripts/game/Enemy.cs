using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var amountToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * amountToMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
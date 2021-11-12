using UnityEngine;

public class Obelisk : MonoBehaviour
{
    public int HP = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            HP -= enemy.Damage;
            Debug.Log($"Obelisk apply damage from {enemy.name}");
        }       
    }
}

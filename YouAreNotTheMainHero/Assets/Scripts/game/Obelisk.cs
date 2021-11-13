using System;
using System.Collections;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    public int HP = 100;
    public State[] States;

    public void Reset()
    {
        HP = 100;
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            UpdateHP(-enemy.Damage);
            UpdateState();

            Debug.Log($"Obelisk apply damage from {enemy.name} HP {HP}");
        }
        else if (other.gameObject.tag == "Healer")
        {
            var healer = other.gameObject.GetComponent<Healer>();
            UpdateHP(healer.HealingPower);
            UpdateState();

            Debug.Log($"Obelisk apply healing HP {HP}");
        }
    }

    private void UpdateHP(int damage)
    {
        if (HP == 0)
            return;

        HP = Mathf.Clamp(HP + damage, 0, 100);

        EventDispatcher.OnHpUpdated?.Invoke(this, new IntEventArgs(HP));

        if (HP == 0)
            StartCoroutine(DispatchEvent());
    }

    private IEnumerator DispatchEvent()
    {
        yield return new WaitForSeconds(1f);

        EventDispatcher.OnGameOver?.Invoke(this, new EventArgs());
    }

    private void UpdateState()
    {
        for (var i = 0; i < States.Length - 1; i++)
            States[i].Model.SetActive(States[i].HP >= HP && HP > States[i + 1].HP);

        States[States.Length - 1].Model.SetActive(HP == 0);
    }
}

[Serializable]
public class State
{
    public int HP;
    public GameObject Model;
}

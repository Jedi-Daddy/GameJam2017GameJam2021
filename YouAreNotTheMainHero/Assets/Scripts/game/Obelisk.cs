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
            HP = Mathf.Max(HP - enemy.Damage, 0);
            UpdateState();

            Debug.Log($"Obelisk apply damage from {enemy.name} HP {HP}");
        }       
    }

    private void UpdateHP(int damage)
    {
        if (HP == 0)
            return;

        HP = Mathf.Max(HP - damage, 0);

        EventDispatcher.OnHpUpdated.Invoke(this, new IntEventArgs(HP));

        if (HP == 0)
            StartCoroutine(DispatchEvent());
    }

    private IEnumerator DispatchEvent()
    {
        yield return new WaitForSeconds(1f);

        EventDispatcher.OnGameOver.Invoke(this, new EventArgs());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public int Damage => curDamage;

    public Level Level;
    public Transform Obelisk;
    public float DamageDelay;
    public float DamageInterval;
    [SerializeField] private int NormalDamage;
    [Header("Loose")]
    public float LooseTime;
    public int LooseDamage;
    public Color LooseColor;
    public GameObject ShadowObject;

    private Coroutine loose;
    private int curDamage;
    private Material material;
    private Color normalColor;

    private void Start()
    {
        curDamage = NormalDamage;
        material = ShadowObject.GetComponent<MeshRenderer>().material;
        normalColor = material.GetColor("_Color");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy_Hathor")
        {
            if (loose != null)
                StopCoroutine(loose);

            loose = StartCoroutine(StartLoose());
        }
    }

    private IEnumerator StartLoose()
    {
        curDamage = LooseDamage;
        material.SetColor("_Color", LooseColor);

        yield return new WaitForSeconds(LooseTime);

        curDamage = NormalDamage;
        material.SetColor("_Color", normalColor);
        loose = null;
    }
}

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

    public float RotationAmount = 2f;
    public int TicksPerSecond = 60;

    public float AngleMinimum = 0;
    public bool IsMoving;
    public int NeededRoration;

    private Coroutine loose;
    private int curDamage;
    private Material material;
    private Color normalColor;

    public Dictionary<int, int> PathRotation = new Dictionary<int, int>
        {
            { 0, 178 },
            { 1, 224 },
            { 2, 268 },
            { 3, 0 },
            { 4, 44 },
            { 5, 90 },
        };

    private void Start()
    {
        curDamage = NormalDamage;
        material = ShadowObject.GetComponent<MeshRenderer>().material;
        normalColor = material.GetColor("_Color");
    }

    private void OnEnable()
    {
        EventDispatcher.OnSunUpdated += OnSunUpdated;
        EventDispatcher.OnStarDirectiontPosition += OnStarDirectiontPosition;
    }

    private void OnDisable()
    {
        EventDispatcher.OnSunUpdated -= OnSunUpdated;
        EventDispatcher.OnStarDirectiontPosition -= OnStarDirectiontPosition;
    }

    public void OnStarDirectiontPosition(object sender, PositionEventArgs args)
    {
        //transform.LookAt(args.Position);
    }

    public void OnSunUpdated(object sender, IntEventArgs args)
    {
        NeededRoration = PathRotation[args.Idx];
        if (!IsMoving)
            StartCoroutine(Rotate(args.IsClockwise));
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

    public IEnumerator Rotate(bool isClockwice)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / TicksPerSecond);

        while (NeedRotation())
        {
            IsMoving = true;
            if (isClockwice)
                transform.Rotate(Vector3.up * RotationAmount);
            else
                transform.Rotate(Vector3.down * RotationAmount);
            yield return wait;
        }

        IsMoving = false;
    }

    private bool NeedRotation()
    {
        var cur = (int)transform.localEulerAngles.y;

        var min = NeededRoration - AngleMinimum;
        if (NeededRoration == 0 && AngleMinimum != 0)
            min = 360 - AngleMinimum;

        var max = NeededRoration + AngleMinimum;

        if (cur == NeededRoration || (cur >= min && cur <= max))
            return false;
        return true;
    }
}

using UnityEngine;

public class Healer : MonoBehaviour
{
    public Transform BasePoint;
    public Transform Target;
    public int KillToHeal = 5;
    public int HealingPower = 20;
    public float Speed = 150f;

    private bool AtHome => Vector3.SqrMagnitude(transform.position - BasePoint.position) < 4500f;

    private bool play;
    private bool back;
    private Vector3 lookpoint;

    private void Start()
    {
        lookpoint = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        transform.LookAt(lookpoint);
    }

    private void OnEnable()
    {
        EventDispatcher.OnScoreUpdated += OnScoreUpdated;
    }

    private void OnDisable()
    {
        EventDispatcher.OnScoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreUpdated(object sender, IntEventArgs args)
    {
        if (args.Value % KillToHeal == 0)
            StartAnimation();
    }

    private void StartAnimation()
    {
        play = true;
    }

    private void Update()
    {
        if (!play)
            return;

        var amountToMove = Speed * Time.deltaTime;
        transform.Translate(Vector3.forward * amountToMove);

        if (back && AtHome)
        {
            play = false;
            back = false;
            transform.LookAt(lookpoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obelisk")
            GoBack();
    }

    private void GoBack()
    {
        back = true;
        transform.LookAt(BasePoint.position);
    }
}
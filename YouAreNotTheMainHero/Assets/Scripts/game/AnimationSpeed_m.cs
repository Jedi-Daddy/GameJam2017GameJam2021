using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AnimationSpeed_m : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        var enemy = GetComponent<Enemy>();

        // Walk backwards
        anim["Base_pers_anim"].speed = enemy.Speed * 0.012f;

        // Walk at double speed
        anim["Base_pers_anim"].speed = enemy.Speed * 0.012f;
    }
}
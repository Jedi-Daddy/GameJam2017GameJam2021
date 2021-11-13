using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        var enemy = GetComponent<Enemy>();

        // Walk backwards
        anim["Base_pers_anim_bastet"].speed = enemy.Speed * 0.012f;

        // Walk at double speed
        anim["Base_pers_anim_bastet"].speed = enemy.Speed * 0.012f;
    }
}
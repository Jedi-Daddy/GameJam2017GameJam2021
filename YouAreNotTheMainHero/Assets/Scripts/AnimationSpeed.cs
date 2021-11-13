using System.Collections;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        // Walk backwards
        anim["Base_pers_anim_bastet"].speed = -1.0f;

        // Walk at double speed
        anim["Base_pers_anim_bastet"].speed = 2.0f;
    }
}
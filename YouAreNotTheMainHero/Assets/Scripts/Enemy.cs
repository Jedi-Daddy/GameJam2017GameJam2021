using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        float speed = 2f;
        void Start()
        {

        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            var direction = new Vector3(1f,0,1f);
            var amountToMove = speed * Time.deltaTime;
            transform.Translate(direction * amountToMove);
        }
    }
}
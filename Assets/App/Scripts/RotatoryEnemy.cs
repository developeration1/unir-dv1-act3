using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatoryEnemy : Enemy
{
    [SerializeField] float speed;
    protected override void Movement()
    {
        transform.Rotate(speed * Vector3.up);
    }
}

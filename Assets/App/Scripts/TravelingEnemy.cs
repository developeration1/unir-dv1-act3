using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingEnemy : Enemy
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float errorMarginRadius;
    
    int current = 0;

    private void Start()
    {
        transform.position = waypoints[0].position;
    }

    protected override void Movement()
    {
        if (Vector3.Distance(waypoints[current].position, transform.position) < errorMarginRadius)
        {
            current++;
            if (current >= waypoints.Length)
                current = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].position, speed * Time.deltaTime);
        transform.Rotate(rotationSpeed * Vector3.up);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(waypoints[i].position, errorMarginRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(waypoints[i == 0 ? ^1 : i - 1].position, waypoints[i].position);
        }
    }
}

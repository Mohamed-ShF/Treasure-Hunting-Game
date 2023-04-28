using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceMovement : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    private int waypointIndex = 0;
    [SerializeField] float speed = 5f;

    void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Time.deltaTime * speed);

    }
}

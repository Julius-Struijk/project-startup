using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class NavigationScript : MonoBehaviour
{
    public Transform _targit;
    public NavMeshAgent agent;
    public float _minDistance;

    void Update()
    {
        double difference = Math.Sqrt(
          Math.Pow(_targit.transform.position.x - transform.position.x, 2f) +
          Math.Pow(_targit.transform.position.z - transform.position.z, 2f));

        if (difference > _minDistance) agent.destination = _targit.position;
        else agent.destination = transform.position;
    }
}

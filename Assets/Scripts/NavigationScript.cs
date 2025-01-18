using UnityEngine.AI;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{
    public Transform _targit;
    public NavMeshAgent agent;
    void Update()
    {
        agent.destination = _targit.position;
    }
}

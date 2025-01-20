using UnityEngine.AI;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{
    public Transform _player;
    public NavMeshAgent agent;

    void Update()
    {
        if (agent != null && agent.enabled)
        {
            agent.destination = _player.position;

            if (agent.stoppingDistance >= agent.remainingDistance)
            {
                transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
            }
        }
        else 
        {
            transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
        }
    }
}

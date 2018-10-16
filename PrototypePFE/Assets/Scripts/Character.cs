using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public NavMeshAgent navmesh;

    private void Awake()
    {
        navmesh = GetComponent<NavMeshAgent>();
    }

	void Start ()
	{
    }

    // Update is called once per frame
    void Update ()
	{
	}

    public bool IsArrived()
    {
        if (!navmesh.pathPending)
        {
            if (navmesh.remainingDistance <= navmesh.stoppingDistance)
            {
                if (!navmesh.hasPath || navmesh.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingObstacle : MonoBehaviour , IStopeable
{
    private Animation anim;
    private NavMeshObstacle navMeshObstacle;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.carveOnlyStationary = false;
    }

    private bool stop = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (stop)
                StopMove();
            else
                StartMove();

            stop = !stop;
        }
    }

    public void StartMove()
    {
        anim.Play();
        navMeshObstacle.carving = false;
    }

    public void StopMove()
    {
        anim.Stop();
        navMeshObstacle.carving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.GetComponent<Character>().Delete();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Character"))
        {
            //Destroy(other.gameObject);
        }
    }
}

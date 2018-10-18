using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider= gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(1, 15, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            Character charac = other.GetComponent<Character>();
            if (charac.@group.destination == transform)
                return;

            charac.@group.SetDesination(transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private BoxCollider boxCollider;
    private List<Group> groups;
    private void Awake()
    {
        groups = new List<Group>();
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

            if (groups.Contains(charac.@group))
                return;

            charac.@group.UpdatePath();
            groups.Add(charac.@group);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject destructibleWallPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
            return;

        Instantiate(destructibleWallPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

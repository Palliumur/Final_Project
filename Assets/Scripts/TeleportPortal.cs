using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    public Transform targetPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetPosition.position;
            Debug.Log("Player teleported to " + targetPosition.position);
            var p = other.GetComponent<PlayerMovement>();
            StartCoroutine(p.WaitForSec(2f));
        }
    }
}

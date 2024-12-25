using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY+1.59f, transform.position.z), ref velocity, speed);
    }

    public void MovetoNextRoom(Transform _nextRoom)
    {
        currentPosX = _nextRoom.position.x;
        currentPosY = _nextRoom.position.y;
        Debug.Log(currentPosX);
    }
}

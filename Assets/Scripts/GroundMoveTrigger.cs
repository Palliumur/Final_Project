using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoveTrigger : MonoBehaviour
{
    public GroundMover groundMover; // 引用 GroundMover 脚本

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            groundMover.MoveGround(); // 触发地面平移
        }
    }
}


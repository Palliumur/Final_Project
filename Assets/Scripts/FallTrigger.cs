using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public FallingObject fallingObject; // 引用下落方块的脚本

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fallingObject.StartFalling(); // 触发方块开始下落
        }
    }
}

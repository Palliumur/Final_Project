using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Vector3 initialPosition; // 记录初始位置
    private bool isFalling = false;  // 标记方块是否正在下落
    public float fallSpeed = 2f;     // 下落速度

    private void Start()
    {
        // 记录初始位置
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            // 方块下落
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    public void StartFalling()
    {
        // 开始下落
        isFalling = true;
    }

    public void ResetPosition()
    {
        // 恢复方块到初始位置并停止下落
        transform.position = initialPosition;
        isFalling = false;
    }
}


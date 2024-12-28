using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialFallingTrigger : MonoBehaviour
{
    public FallingObject[] fallingObjects;  // 要按次序下落的方块
    public float fallInterval = 0.5f;      // 每个方块下落的间隔时间
    private bool isFalling = false;        // 检测是否正在下落
    private Coroutine fallCoroutine;        // 用于存储协程引用

    private void Start()
    {
        PlayerRespawn.instance.onPlayerRespawn.AddListener(StopFalling);
    }

    // 触发器进入时，开始按次序下落方块
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling)  // 确保是玩家进入且没有正在下落
        {
            isFalling = true;
            fallCoroutine = StartCoroutine(FallBlocksSequentially());  // 启动方块下落协程
        }
    }

    // 按次序下落方块
    IEnumerator FallBlocksSequentially()
    {
        foreach (var fallingObject in fallingObjects)
        {
            if (fallingObject != null)
            {
                fallingObject.StartFalling();  // 启动单个方块的下落过程
                yield return new WaitForSeconds(fallInterval);  // 等待一定时间后启动下一个方块的下落
            }
        }
        isFalling = false;  // 所有方块下落完毕后重置下落状态
    }

    // 停止所有正在下落的方块
    private void StopFalling()
    {
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null;
        }

        foreach (var fallingObject in fallingObjects)
        {
            if (fallingObject != null)
            {
                fallingObject.StopFalling();
            }
        }

        isFalling = false;
    }
}
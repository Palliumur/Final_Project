using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public float moveDistance = 5f; // 平移的距离
    public float moveDuration = 1f; // 平移的持续时间

    private Vector3 initialPosition; // 初始位置
    private Vector3 targetPosition;  // 目标位置
    private bool hasMoved = false;   // 标志，确保地面只移动一次

    void Start()
    {
        initialPosition = transform.position; // 记录地面初始位置
        targetPosition = initialPosition + Vector3.left * moveDistance; // 计算目标位置
    }

    // 触发地面平移
    public void MoveGround()
    {
        if (!hasMoved)
        {
            hasMoved = true;
            StartCoroutine(MoveGroundCoroutine());
        }
    }

    // 重置地面位置
    public void ResetGroundPosition()
    {
        transform.position = initialPosition; // 将地面位置恢复到初始位置
        hasMoved = false; // 重置移动标志
    }

    private IEnumerator MoveGroundCoroutine()
    {
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;

        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // 确保地面完全到达目标位置
    }
}


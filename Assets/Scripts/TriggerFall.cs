using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    public GameObject targetObject; // 目标对象（会掉下去的物体）
    public float fallSpeed = 2f;    // 掉落速度
    public float fallDistance = 5f; // 掉落的总距离

    private bool isFalling = false; // 标记是否开始掉落
    private Vector3 initialPosition; // 记录初始位置

    private void Start()
    {
        // 记录目标物体的初始位置
        if (targetObject != null)
        {
            initialPosition = targetObject.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是 Player 触发的
        if (other.CompareTag("Player"))
        {
            isFalling = true; // 开始掉落
        }
    }

    private void Update()
    {
        // 如果物体开始掉落
        if (isFalling && targetObject != null)
        {
            // 按照指定速度下落
            targetObject.transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);

            // 如果下落距离达到指定值，停止掉落
            if (initialPosition.y - targetObject.transform.position.y >= fallDistance)
            {
                isFalling = false; // 停止掉落
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPosition; // 复活位置
    // public GameObject deathEffect;   // 可选：死亡特效
    public FallingObject[] fallingObjects; // 引用所有下落的方块

    private void Start()
    {
        respawnPosition = transform.position; // 初始复活点
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            Die(); // 玩家死亡
        }
        else if (other.CompareTag("RespawnZone"))
        {
            respawnPosition = other.transform.position; // 更新复活点
        }
    }

    private void Die()
    {
        // 可选：播放死亡特效
        // if (deathEffect != null)
        // {
        //     Instantiate(deathEffect, transform.position, Quaternion.identity);
        // }

        // 恢复玩家到复活点
        transform.position = respawnPosition;

        // 重置所有下落方块的位置
        foreach (FallingObject obj in fallingObjects)
        {
            obj.ResetPosition();
        }

        // 重置玩家速度，防止惯性影响
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}

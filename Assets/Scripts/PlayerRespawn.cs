using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn instance;
    private Vector3 respawnPosition; // 复活位置
    // public GameObject deathEffect;   // 可选：死亡特效
    public FallingObject[] fallingObjects; // 引用所有下落的方块
    public GroundMover[] groundMovers; // 引用所有地面控制器
    public UnityEvent onPlayerRespawn;
    public GameObject RestartUI;
    private bool isDead;
    public AudioClip dieSFX, respawnSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isDead = false;
        respawnPosition = transform.position; // 初始复活点
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            if (isDead) return;
            isDead = true;
            StartCoroutine(Die());
        }
        else if (other.CompareTag("RespawnZone"))
        {
            respawnPosition = other.transform.position; // 更新复活点
        }
    }

    private IEnumerator Die()
    {
        RestartUI.SetActive(true);

        audioSource.clip = dieSFX;
        audioSource.Play();

        // 停止所有正在下落的方块

        yield return WaitForAnyInput();

        Respawn();
    }

    private IEnumerator WaitForAnyInput()
    {
        while (true)
        {
            if (Input.anyKeyDown) break;
            yield return null;
        }
    }

    private void Respawn()
    {
        isDead = false;
        RestartUI.SetActive(false);

        audioSource.clip = respawnSFX;
        audioSource.Play();

        StopAllFallingObjects();

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

        // 重置所有地面
        foreach (GroundMover groundMover in groundMovers)
        {
            groundMover.ResetGroundPosition(); // 归位地面
        }

        onPlayerRespawn.Invoke();
    }

    private void StopAllFallingObjects()
    {
        foreach (FallingObject obj in fallingObjects)
        {
            if (obj != null)
            {
                obj.StopFalling();
            }
        }
    }
}
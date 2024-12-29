using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn instance;
    private Vector3 respawnPosition;
    public FallingObject[] fallingObjects;
    public GroundMover[] groundMovers;
    public UnityEvent onPlayerRespawn;
    public GameObject RestartUI;
    private bool isDead;
    public AudioClip dieSFX, respawnSFX;
    private AudioSource audioSource;
    public bool hasEatenApple = false;
    private bool isInAppleTime = false;
    private bool diedAfterApple = false;
    public bool notDiedAfterApple = false;
    public GameObject Apple;

    // 新增重生计数
    public int respawnCount = 0;  // 记录重生次数
    private bool achievement1Triggered = false;  // 用于确保成就只触发一次
    private bool achievement2Triggered = false;
    public GameObject achievementUI1;  // 用于显示成就弹框
    public GameObject achievementUI2;
    public GameObject achievementUI3;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isDead = false;
        respawnPosition = transform.position;
        achievementUI1.SetActive(false);  // 初始时隐藏成就弹框
        achievementUI2.SetActive(false);
        achievementUI3.SetActive(false);
    }
    private void Update()
    {
        if (notDiedAfterApple && !achievement2Triggered)
        {
            ShowAchievementUI3();
            achievement2Triggered = true;  // 确保成就只触发一次
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            if (isDead) return;
            isDead = true;
            if (isInAppleTime && !achievement2Triggered)
            {
                diedAfterApple = true;  // 标记玩家死亡后是否在2秒内
            }
            StartCoroutine(Die());
        }
        else if (other.CompareTag("RespawnZone"))
        {
            respawnPosition = other.transform.position;
        }
    }
    public void StartAppleTimer()
    {
        if (hasEatenApple)
        {
            isInAppleTime = true;
            StartCoroutine(AppleTimer());
        }
    }
    private IEnumerator AppleTimer()
    {
        yield return new WaitForSeconds(2f); // 等待2秒
        isInAppleTime = false;
        notDiedAfterApple = true;
    }

    private IEnumerator Die()
    {
        RestartUI.SetActive(true);

        audioSource.clip = dieSFX;
        audioSource.Play();

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
        transform.position = respawnPosition;
        if (Apple != null)
        {
            Apple.SetActive(false);
        }
        foreach (FallingObject obj in fallingObjects)
        {
            obj.ResetPosition();
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        foreach (GroundMover groundMover in groundMovers)
        {
            groundMover.ResetGroundPosition();
        }

        // 增加重生次数
        respawnCount++;

        // 检查是否已经达到 10 次重生
        if (respawnCount >= 10 && !achievement1Triggered)
        {
            ShowAchievementUI1();
            achievement1Triggered = true;  // 确保成就只触发一次
        }
        else if (diedAfterApple && !achievement2Triggered)
        {
            ShowAchievementUI2();
            achievement2Triggered = true;  // 确保成就只触发一次
        }
        else
        {
            onPlayerRespawn.Invoke();
        }
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

    // 显示成就弹框
    private void ShowAchievementUI1()
    {
        achievementUI1.SetActive(true);
        Time.timeScale = 0;  // 暂停游戏
    }
    private void ShowAchievementUI2()
    {
        achievementUI2.SetActive(true);
        Time.timeScale = 0;  // 暂停游戏
    }
    private void ShowAchievementUI3()
    {
        achievementUI3.SetActive(true);
        Time.timeScale = 0;  // 暂停游戏
    }

// 按 Enter 键关闭成就弹框
public void CloseAchievementUI()
    {
        achievementUI1.SetActive(false);
        achievementUI2.SetActive(false);
        achievementUI3.SetActive(false);
        Time.timeScale = 1;  // 恢复游戏
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private bool hasPlayedAnim = false;
    private UIManager uiManager;
    public GameObject AchievementUI;
    private PlayerRespawn playerRespawn;

    private void Awake() 
    {
        uiManager = FindObjectOfType<UIManager>();
        playerRespawn = FindObjectOfType<PlayerRespawn>();
    }
    private void Start()
    {
        AchievementUI.SetActive(false);  // 初始时隐藏成就弹框
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            if (hasPlayedAnim) return;
            hasPlayedAnim = true;
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Animator>().SetTrigger("Appear");
            GetComponent<PlayerMovement>().movable = false;
            if (playerRespawn.respawnCount == 0)
            {
                StartCoroutine(ShowAchievementAndVictory());
            }
            else
            {
                uiManager.ShowVictoryUI();
                Victory.instance.PlaySound();
            }
            
        }
    }
    private IEnumerator ShowAchievementAndVictory()
    {
        // 显示成就弹框
        ShowAchievementUI();
        Time.timeScale = 0;  // 暂停游戏

        // 等待玩家按下 Enter 键来关闭成就弹框
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        // 关闭成就弹框并恢复游戏
        uiManager.CloseAchievementUI();
        Time.timeScale = 1;

        // 显示胜利界面
        uiManager.ShowVictoryUI();
        Victory.instance.PlaySound();
    }
    public void ShowAchievementUI()
    {
        AchievementUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseAchievementUI()
    {
        AchievementUI.SetActive(false);
        Time.timeScale = 1;
    }
}

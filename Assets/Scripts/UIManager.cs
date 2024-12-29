using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Victory")]
    [SerializeField] private GameObject victoryUI;

    [Header("Pause")]
    [SerializeField] private GameObject pauseUI;

    [Header("Achievement1")]
    [SerializeField] private GameObject achievement1UI;  // 成就弹框
 
    [Header("Achievement2")]
    [SerializeField] private GameObject achievement2UI;  // 成就弹框

    [Header("Achievement3")]
    [SerializeField] private GameObject achievement3UI;  // 成就弹框

    [Header("Achievement4")]
    [SerializeField] private GameObject achievement4UI;  // 成就弹框

    private void Start()
    {
        victoryUI.SetActive(false);
        pauseUI.SetActive(false);
        achievement1UI.SetActive(false);  // 初始时隐藏成就弹框
        achievement2UI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (pauseUI.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);

        // 按 Enter 键关闭成就弹框
        if (Input.GetKeyDown(KeyCode.Return) && (achievement1UI.activeInHierarchy || achievement2UI.activeInHierarchy || achievement3UI.activeInHierarchy || achievement4UI.activeInHierarchy))
        {
            CloseAchievementUI();
        }
    }

    #region "Victory UI"
    public void ShowVictoryUI()
    {
        victoryUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Pausegame()
    {
        PauseGame(true);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region "Pause UI"
    public void PauseGame(bool status)
    {
        pauseUI.SetActive(status);
        Time.timeScale = status ? 0 : 1;
    }
    #endregion

    #region "Achievement UI"
    // 关闭成就弹框
    public void CloseAchievementUI()
    {
        achievement1UI.SetActive(false);
        achievement2UI.SetActive(false);
        achievement3UI.SetActive(false);
        achievement4UI.SetActive(false);
        Time.timeScale = 1;  // 恢复游戏
    }
    #endregion
}

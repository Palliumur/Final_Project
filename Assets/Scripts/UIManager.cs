using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Victory")] 
    [SerializeField] private GameObject victoryUI;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseUI;

    private void Start()
    {
        victoryUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (pauseUI.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
    }
    #region "Victory UI"
    public void ShowVictoryUI()
    {
        victoryUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}

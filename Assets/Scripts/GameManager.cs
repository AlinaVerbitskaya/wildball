using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private Canvas gameOverlay;
    [SerializeField] private Canvas pauseScreen;
    [SerializeField] private Canvas playerDeadScreen;
    [SerializeField] private Canvas winScreen;

    [Header("LevelScore")]
    [SerializeField] private Text scoreTextField;
    [SerializeField] private int maxScoreOnLevel = 1;
    private int currentScore = 0;

    void Start()
    {
        GetReady();    
    }
    private void OnEnable()
    {
        EventManager.OnCrystalCollected += IncreaseLevelScore;
        EventManager.OnLevelFinished += LevelFinish;
    }
    private void OnDisable()
    {
        EventManager.OnCrystalCollected -= IncreaseLevelScore;
        EventManager.OnLevelFinished -= LevelFinish;
    }

    private void GetReady()
    {
        gameOverlay.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(true);
        playerDeadScreen.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(true);
        gameOverlay.enabled = true;
        pauseScreen.enabled = false;
        playerDeadScreen.enabled = false;
        winScreen.enabled = false;
        scoreTextField.text = $"Score: 0/{maxScoreOnLevel}";
        PauseToggle(false);
    }

    public void PauseToggle(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        gameOverlay.enabled = !pause;
        pauseScreen.enabled = pause;
    }

    public void ShowDeathScreen()
    {
        playerDeadScreen.enabled = true;
    }

    public static void LoadScene(int index) 
    {
        SceneManager.LoadScene(index);
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelFinish()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex == SceneManager.sceneCountInBuildSettings - 1) //level is the last one
        {
            StartCoroutine("GameFinished");
        }
        else
        {
            SceneManager.LoadScene(currentLevelIndex + 1); //go to next level
        }
    }

    private void IncreaseLevelScore()
    {
        scoreTextField.text = $"Score: {++currentScore}/{maxScoreOnLevel}";
    }

    private IEnumerator GameFinished()
    {
        winScreen.enabled = true;
        winScreen.GetComponent<WinScreen>().PlayParticles();
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(0);
    }
}

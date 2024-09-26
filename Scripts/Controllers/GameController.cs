using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] public int coinsCollected;
    [SerializeField] public int coinsToNextLevel = 10;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text nextLevelText;
    [SerializeField] public float surviveTime;
    [SerializeField] private int gameLevel = 1;
    [SerializeField] private int sceneIndex = 0;
    private bool nextLevel;
    public int coinsToPowerUp;

    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        GetPlayerPrefs();
        nextLevelText.enabled = false;

        if (gameLevel < 1)
        {
            gameLevel = 1;
            surviveTime = 0;
            coinsToNextLevel = 10;
        }
        nextLevelText.enabled = false;
        StopAllCoroutines();
    }

    void Update()
    {
        ShowCollectedCoins();
        ShowSurviveTime();
        ShowGameLevel();
        if(coinsCollected == coinsToNextLevel)
        {
            nextLevel = true;
            nextLevelText.enabled = true;
            sceneIndex++;
            if (nextLevel)
            {
                nextLevelText.text = "Next Level!";
                NextLevel(sceneIndex);
            }
            
        }
    }

    private void ShowCollectedCoins()
    {
        coinsText.text = $"x {coinsCollected}";
    }
    private void ShowSurviveTime()
    {
        surviveTime += Time.deltaTime;
        timeText.text = $"Survive: {Mathf.Round(surviveTime)}";
    }

    private void ShowGameLevel()
    {
        levelText.text = $"Level: {gameLevel}";
    }

    private void NextLevel(int sceneIndex)
    {
        if(gameLevel < 3)
        {
            coinsToNextLevel += 10;
            StartCoroutine(LevelTransition());
        }
    }

    private IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(2f);
        gameLevel++;
        SetPlayerPrefs();
        SceneManager.LoadScene(sceneIndex);
        StartCoroutine(LevelTransition());
    }

    public void GetPlayerPrefs()
    {
        coinsCollected = PlayerPrefs.GetInt("coinsCollected");
        surviveTime =  PlayerPrefs.GetFloat("surviveTime");
        gameLevel = PlayerPrefs.GetInt("gameLevel");
        coinsToNextLevel  = PlayerPrefs.GetInt("coinsToNextLevel");
    }


    public void RestartPlayerPrefs()
    {
        PlayerPrefs.SetInt("isStarted", 0);
        PlayerPrefs.SetInt("coinsCollected", 0);
        PlayerPrefs.SetFloat("surviveTime", 0);
        PlayerPrefs.SetInt("gameLevel", 0);
        PlayerPrefs.SetInt("coinsToNextLevel", 0);
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("isStarted",1);
        PlayerPrefs.SetInt("coinsToNextLevel", coinsToNextLevel);
        PlayerPrefs.SetInt("coinsCollected", coinsCollected);
        PlayerPrefs.SetFloat("surviveTime", surviveTime);
        PlayerPrefs.SetInt("gameLevel", gameLevel);
    }

}

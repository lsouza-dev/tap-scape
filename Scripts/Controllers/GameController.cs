using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    [SerializeField] public TMP_Text nextLevelText;
    [SerializeField] public float surviveTime;
    [SerializeField] public int gameLevel = 1;
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] GameObject ruby;
    Player player;
    private bool nextLevel;
    public float secondsToPowerUp;
    public bool gameEnd;
    // Start is called before the first frame update
    // Update is called once per frame


    private void Awake()
    {
        player = FindObjectOfType<Player>(); 
    }
    private void Start()
    {
        GetPlayerPrefs();
        nextLevelText.enabled = false;

        if (gameLevel < 1)
        {
            gameLevel = 1;
            surviveTime = 0;
            coinsToNextLevel += 10;
        }
        nextLevelText.enabled = false;
        StopAllCoroutines();
    }

    void Update()
    {
        ShowCollectedCoins();
        ShowSurviveTime();
        ShowGameLevel();
        print(gameLevel);
        if(coinsCollected == coinsToNextLevel)
        {
            nextLevel = true;
            nextLevelText.enabled = true;
            sceneIndex++;
            if (nextLevel)
            {
                if (gameLevel < 3)
                {
                    nextLevelText.text = "Next Level!";
                    NextLevel();
                }
                else
                {
                    SpawnController spawn = FindObjectOfType<SpawnController>();
                    spawn.isSpawning = false;
                    gameEnd = true;
                }
            }          
        }

        secondsToPowerUp += Time.deltaTime;
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

    private void NextLevel()
    {
        if(gameLevel < 3 && !gameEnd)
        {
            coinsToNextLevel += 10;
            StartCoroutine(LevelTransition());
        }
    }

    private IEnumerator LevelTransition()
    {
        if(gameLevel < 3)
        {
            yield return new WaitForSeconds(2f);
            gameLevel++;
            SetPlayerPrefs();
            SceneManager.LoadScene(sceneIndex);
            StartCoroutine(LevelTransition());
        }
        
    }

    public void GetPlayerPrefs()
    {
        coinsCollected = PlayerPrefs.GetInt("coinsCollected");
        surviveTime =  PlayerPrefs.GetFloat("surviveTime");
        gameLevel = PlayerPrefs.GetInt("gameLevel");
        coinsToNextLevel  = PlayerPrefs.GetInt("coinsToNextLevel");
        player.haveShield = PlayerPrefs.GetInt("haveShield");
        sceneIndex = PlayerPrefs.GetInt("sceneIndex");
    }


    public void RestartPlayerPrefs()
    {
        PlayerPrefs.SetInt("isStarted", 0);
        PlayerPrefs.SetInt("coinsCollected", 0);
        PlayerPrefs.SetFloat("surviveTime", 0);
        PlayerPrefs.SetInt("gameLevel", 0);
        PlayerPrefs.SetInt("coinsToNextLevel", 0);
        PlayerPrefs.SetInt("haveShield", 0);
        PlayerPrefs.SetInt("sceneIndex", 0);
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("isStarted",1);
        PlayerPrefs.SetInt("coinsToNextLevel", coinsToNextLevel);
        PlayerPrefs.SetInt("coinsCollected", coinsCollected);
        PlayerPrefs.SetFloat("surviveTime", surviveTime);
        PlayerPrefs.SetInt("gameLevel", gameLevel);
        PlayerPrefs.SetInt("sceneIndex", sceneIndex);
    }

}

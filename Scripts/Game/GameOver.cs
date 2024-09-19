using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameController controller;
    [SerializeField] private GameObject menu;
    [SerializeField] private int coins;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text highscoreText;
    // Start is called before the first frame update
    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
    }

    void Start()
    {
        coins = controller.coinsCollected;

        if (coins > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", coins);
        }

        scoreText.text = $"Score: {controller.coinsCollected}";
        highscoreText.text = $"Highscore: {PlayerPrefs.GetInt("highscore")}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
        PlayerPrefs.SetInt("isStarted", 0);
    }
}

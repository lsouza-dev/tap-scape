using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameController controller;
    [SerializeField] public TMP_Text score;
    // Start is called before the first frame update
    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
    }

    void Start()
    {
        score.text = $"Score: {controller.coinsCollected}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

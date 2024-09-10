using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] public int coinsCollected;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] public float surviveTime;

    public int coinsToPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowCollectedCoins();
        ShowSurviveTime();
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

}

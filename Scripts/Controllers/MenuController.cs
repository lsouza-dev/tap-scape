using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text survive;
    [SerializeField] private GameObject coinImage;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    private void Awake()
    {
        renderer = FindObjectOfType<Player>().GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("isStarted") == 1)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
        else
        {
            renderer.enabled = false;
            score.enabled = false;
            survive.enabled = false;
            coinImage.SetActive(false);
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
        PlayerPrefs.SetInt("isStarted", 0);
    }

    public void PlayGame()
    {
        gameObject.SetActive(false);
        score.enabled = true;
        survive.enabled = true;
        coinImage.SetActive(true);
        renderer.enabled = true;
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("isStarted", 1);
    }
}

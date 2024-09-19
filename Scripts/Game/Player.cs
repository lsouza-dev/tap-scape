using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;


    [Header("Player Attributes")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private bool isShield;


    [Header("GameObjects")]
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject shield;
    //[SerializeField] private AudioSource cameraSource;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gameOver.SetActive(false);
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float xInput = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        
        if(xInput < 0)
        {
            spriteRenderer.flipX = true;
            animator.Play("playerRunning");
        }
        else if(xInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.Play("playerRunning");
        }
        else
        {
            animator.Play("playerIdle");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up, ForceMode2D.Impulse);    
        }

        transform.position = new Vector2(transform.position.x + xInput, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Saw"))
        {
            if (isShield)
            {
                audioSource.Play();
                Destroy(other.gameObject);
                isShield = false;
                shield.SetActive(false);
            }
            else
            {
                MusicController music = FindObjectOfType<MusicController>();
                music.PlayGameOverSound();
                spriteRenderer.enabled = false;
                gameOver.SetActive(true);
                Time.timeScale = 0;
                Destroy(gameObject, timeToDestroy);
            }
            
        }

        if (other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);
            shield.SetActive(true);
            isShield = true;
        }

    }
}

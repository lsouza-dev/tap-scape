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
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float timeToDestroy;
    public int haveShield;
    [SerializeField] private bool isShield;
    [SerializeField] private bool isOnGround;



    [Header("GameObjects")]
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject groundCollider;
    //[SerializeField] private AudioSource cameraSource;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        groundCollider = GameObject.FindGameObjectWithTag("GroundCollider");
    }

    void Start()
    {
        haveShield = PlayerPrefs.GetInt("haveShield");

        if(haveShield == 1)
        {
            isShield = true;
            shield.SetActive(true);
        }
        else
        {
            isShield = false;
            shield.SetActive(false);
        }

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
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else if(xInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        if(transform.position.y <= groundCollider.transform.position.y)
        {
            isOnGround = true;
            animator.SetBool("isJumping", false);
        }
        else
        {
            isOnGround = false;
            animator.SetBool("isJumping", true);
            animator.SetBool("isIdle", false);        }

        if (isOnGround && Input.GetKeyDown(KeyCode.Space) || isOnGround && Input.GetKeyDown(KeyCode.UpArrow) || isOnGround && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * playerSpeed;
            animator.SetBool("isJumping", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", false);
        }
        

        transform.position = new Vector2(transform.position.x + xInput, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Saw") || other.CompareTag("Enemies"))
        {
            if (isShield)
            {
                PlayerPrefs.SetInt("haveShield", 0);
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
            PlayerPrefs.SetInt("haveShield", 1);
            Destroy(other.gameObject);
            shield.SetActive(true);
            isShield = true;
        }

        if (other.CompareTag("Ruby"))
        {
            gameController.nextLevelText.text = "Parabens!!\nVoce completou o jogo!";
        }
    }
}

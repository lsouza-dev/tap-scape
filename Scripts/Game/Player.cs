using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Player Attributes")]
    [SerializeField] private float playerSpeed;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject gameOver;
    //[SerializeField] private AudioSource cameraSource;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        //cameraSource = FindObjectOfType<Camera>().GetComponent<AudioSource>();
    }

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
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
            spriteRenderer.flipX = false;
            animator.Play("playerIdle");
        }
        
        
        transform.position = new Vector2(transform.position.x + xInput, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Saw"))
        {
            audioSource.Play();
            Destroy(other.gameObject);
            spriteRenderer.enabled = false;
            gameOver.SetActive(true);
            Destroy(gameObject, timeToDestroy);

        }
    }
}

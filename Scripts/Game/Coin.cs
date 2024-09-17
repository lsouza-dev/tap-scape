using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameController gameController;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float timeToDestroy;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        animator.Play("coinRotation");
        Destroy(gameObject,4f);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            spriteRenderer.enabled = false;
            collider.enabled = false;

            gameController.coinsCollected += 1;
            gameController.coinsToPowerUp += 1;

            Destroy(gameObject,timeToDestroy);
        }
        else if (other.CompareTag("Ground"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
        }
    }
}

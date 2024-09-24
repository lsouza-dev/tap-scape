using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour 
{
    [SerializeField] public bool spawnAtRight;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private float enemySpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        enemySpeed = 5;

        if (spawnAtRight)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (spawnAtRight)
        {
            rb.velocity = Vector2.left * enemySpeed;
        }
        else
        {
            rb.velocity = Vector2.right * enemySpeed;
        }
    }
}

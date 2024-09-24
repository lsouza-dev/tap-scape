using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();
    [SerializeField] private List<GameObject> powerUps = new List<GameObject>();
    [SerializeField] private List<Enemies> enemies = new List<Enemies>();
    [SerializeField] private List<Transform> spawnersPos = new List<Transform>();
    [SerializeField] private int timeToSpawnEnemy;
    private static int spawnEnemy;
    [SerializeField] private float enemyXOffset = 13;
    // Start is called before the first frame update
    public bool isSpawning = true;

    private GameController gameController;
    [SerializeField] private float timeToSpawn;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    void Start()
    {
        StartCoroutine(spawnItems());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnItems()
    {
        while (isSpawning)
        {
            if(gameController.coinsToPowerUp == 10)
            {
                int randomPowerUp = Random.Range(0, powerUps.Count);
                GameObject powerUp = Instantiate(powerUps[randomPowerUp], new Vector2(Random.Range(-xOffset, xOffset), yOffset), Quaternion.identity);
                gameController.coinsToPowerUp = 0;
            }
            else
            {
                yield return new WaitForSeconds(timeToSpawn);
                int randomObject = Random.Range(0, spawnables.Count);
                GameObject spawnedObject = Instantiate(spawnables[randomObject], new Vector2(Random.Range(-xOffset, xOffset), yOffset), Quaternion.identity);
            }

            spawnEnemy += 1;

            if (spawnEnemy == timeToSpawnEnemy)
            {
                print("Spawna Inimigo");
                int randomEnemy = Random.Range(0, enemies.Count);
                int randomPos = Random.Range(0, spawnersPos.Count);
                Enemies spawnedEnemy = Instantiate(enemies[randomEnemy], spawnersPos[randomPos].position, spawnersPos[randomPos].rotation);
                
                if(randomPos == 0)
                {
                    spawnedEnemy.spawnAtRight = false;
                    //spawnedEnemy.transform.position = new Vector2(-15f,spawnerTransform.position.y);
                }
                else
                {
                    spawnedEnemy.spawnAtRight = true;
                }
                spawnEnemy = 0;
            }
        }
    }
}

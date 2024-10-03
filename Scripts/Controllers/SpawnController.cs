using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();
    [SerializeField] private List<GameObject> powerUps = new List<GameObject>();
    [SerializeField] private List<Enemies> enemies = new List<Enemies>();
    [SerializeField] private List<Transform> spawnersPos = new List<Transform>();
    [SerializeField] private int timeToSpawnEnemy = 3;
    [SerializeField] private static int spawnEnemy;
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

    private IEnumerator spawnItems()
    {
        while (isSpawning)
        {
            if(gameController.secondsToPowerUp >= 20)
            {
                int randomPowerUp = Random.Range(0, powerUps.Count);
                GameObject powerUp = Instantiate(powerUps[randomPowerUp], new Vector2(Random.Range(-xOffset, xOffset), yOffset), Quaternion.identity);
                gameController.secondsToPowerUp = 0;
            }
            else
            {
                yield return new WaitForSeconds(timeToSpawn);
                int randomObject = Random.Range(0, spawnables.Count -1);
                if (gameController.gameLevel == 1) randomObject = 0;
                GameObject spawnedObject = Instantiate(spawnables[randomObject], new Vector2(Random.Range(-xOffset, xOffset), yOffset), Quaternion.identity);
            }

            if(gameController.gameLevel > 2) spawnEnemy += 1;

            if (spawnEnemy == timeToSpawnEnemy && gameController.gameLevel > 2)
            {
                int randomEnemy = Random.Range(0, enemies.Count);
                int randomPos;

                if(randomEnemy <= 1)
                {
                    randomPos = 0;
                }
                else
                {
                    randomPos = 1;
                }

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

        if (!isSpawning && gameController.gameEnd)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(spawnables[spawnables.Count-1], new Vector2(0, 7.3f), Quaternion.identity);
            StopCoroutine("spawnItens");
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();
    // Start is called before the first frame update
    public bool isSpawning = true;

    [SerializeField] private float timeToSpawn;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
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
            yield return new WaitForSeconds(timeToSpawn);

            int randomObject = Random.Range(0, spawnables.Count);
            GameObject spawnedObject = Instantiate(spawnables[randomObject],new Vector2(Random.Range(-xOffset, xOffset), yOffset),Quaternion.identity);
        }
    }
}

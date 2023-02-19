using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject tripleShotPowerupPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private float spawnRate = 1;

    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    public float randomNum()
    {
        return Random.Range(-7.5f, 7.5f);
    }

    public GameObject Respawn(GameObject respawnObject)
    {
        Instantiate(respawnObject, new Vector3(randomNum(), 6.5f, 0), Quaternion.identity);
        return this.gameObject;
    }
    public GameObject Respawn(GameObject respawnObject, Transform parent)
    {
        Instantiate(respawnObject, new Vector3(randomNum(), 6.5f, 0), Quaternion.identity, parent);
        return this.gameObject;
    }

    // Spawn new enemy every 5 seconds
    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            if (enemy != null)
            {
                Respawn(enemy, enemyContainer.transform);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (stopSpawning == false)
        {
            Respawn(tripleShotPowerupPrefab);
            yield return new WaitForSeconds(Random.Range(10, 16));
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}

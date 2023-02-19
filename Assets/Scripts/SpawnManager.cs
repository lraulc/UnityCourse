using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnRate = 1;
    [SerializeField] private GameObject tripleShotPowerupPrefab;
    [SerializeField] private GameObject speedBoostPowerupPrefab;

    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnSpeedBoostRoutine());
    }

    public GameObject Respawn(GameObject respawnObject)
    {
        Instantiate(respawnObject, new Vector3(Random.Range(-7.5f, 7.5f), 6.5f, 0), Quaternion.identity);
        return this.gameObject;
    }
    public GameObject Respawn(GameObject respawnObject, Transform parent)
    {
        Instantiate(respawnObject, new Vector3(Random.Range(-7.5f, 7.5f), 6.5f, 0), Quaternion.identity, parent);
        return this.gameObject;
    }

    // Spawn new enemy every 5 seconds - Spawn Rate variable used to define spawn speed
    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            if (enemy != null)
            {
                Respawn(enemy, enemyContainer.transform);
                yield return new WaitForSeconds(enemySpawnRate);
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

    IEnumerator SpawnSpeedBoostRoutine()
    {
        while (stopSpawning == false)
        {
            Respawn(speedBoostPowerupPrefab);
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}

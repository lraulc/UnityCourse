using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnRate = 1;

    [SerializeField] private GameObject[] powerups;




    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
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
        yield return new WaitForSeconds(3.0f);

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
        yield return new WaitForSeconds(3.0f);

        while (stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, powerups.Length);
            Respawn(powerups[randomPowerup]);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}

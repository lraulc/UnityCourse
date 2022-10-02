using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private float spawnRate = 1;

    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        StartCoroutine(SpawnRoutine());
    }

    // Spawn new enemy every 5 seconds
    IEnumerator SpawnRoutine()
    {
        while (stopSpawning == false)
        {
            if (enemy != null)
            {
                GameObject newEnemy = enemy.GetComponent<Enemy>().Respawn(enemyContainer.transform);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float startSpawnTime;
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject[] spawnObj;
    
    void Start()
    {
        InvokeRepeating("SpawnPowerUp", startSpawnTime, spawnTime);
    }

    void SpawnPowerUp()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            CancelInvoke();
            return;
        }
        
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnObjIndex = Random.Range(0, spawnObj.Length);
        Instantiate(spawnObj[spawnObjIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}

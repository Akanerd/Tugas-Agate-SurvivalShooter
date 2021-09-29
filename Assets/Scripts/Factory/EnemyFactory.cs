using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{

    [SerializeField]
    public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag, Transform spawnPoint)
    {
        //spawn enemy random
        GameObject enemy = Instantiate(enemyPrefab[tag],spawnPoint.position,spawnPoint.rotation);
        return enemy;
    }
}
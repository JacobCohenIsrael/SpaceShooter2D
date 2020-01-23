using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float SpawnDelayInSeconds = 1.0f;
    [SerializeField] private Transform enemyContainer;

    private bool isSpawning;


    void Start()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(SpawnDelayInSeconds);
            Instantiate<Enemy>(enemyPrefab, enemyContainer);
        }
    }

    internal void StopSpawning()
    {
        isSpawning = false;
    }
}

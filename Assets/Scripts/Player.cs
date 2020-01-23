using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";

    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private float horizontalSpeedMultiplier = 1.0f;
    [SerializeField] private float verticalSpeedMultiplier = 1.0f;

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private GameObject fireGameObject;

    [SerializeField] private float fireRate = 0.25f;

    [SerializeField] SpawnManager spawnManager;

    private float nextFiredTime;
    private Health myHealth;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        nextFiredTime = 0;
        myHealth = GetComponent<Health>();
        myHealth.OnDamageTaken.AddListener(OnDamageTaken);
    }

    private void OnDamageTaken(int damageTaken, int newHealth)
    {
        if (newHealth <= 1)
        {
            fireGameObject.SetActive(true);
        }
    }

    void Update()
    {
        Move();
        CheckBounds();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFiredTime)
        {
            Instantiate<Projectile>(projectilePrefab, projectileSpawn.position, Quaternion.identity);
            nextFiredTime = Time.time + fireRate;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput * horizontalSpeedMultiplier, verticalInput * verticalSpeedMultiplier, 0) * speedMultiplier * Time.deltaTime);
    }

    private void CheckBounds()
    {
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -3.5)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        }

        if (transform.position.x > 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }
    }

    private void OnDestroy()
    {
        Instantiate<Explosion>(explosionPrefab, transform.position, Quaternion.identity);
        spawnManager.StopSpawning();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const string PROJECTILE_TAG = "Projectile";

    [SerializeField] private float initialSpeed = 1.0f;
    [SerializeField] private float ramp = 1.025f;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        speed *= ramp;
    }
}

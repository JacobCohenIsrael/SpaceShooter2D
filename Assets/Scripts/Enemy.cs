using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public const string ENEMY_TAG = "Enemy";

    private const string TRIGGER_ON_ENEMY_DESTROYED = "OnEnemyDestroyed";

    [SerializeField] private float speed = 1.0f;

    private Animator myAnimator;
    private BoxCollider2D myBoxCollider;

    private bool isDestroyed;

    void Start()
    {
        SetNewRandomStartPosition();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        isDestroyed = false;

        if (null == myAnimator)
        {
            Debug.Log("Enemy animator componenet does not exist");
        }

        if (null == myBoxCollider)
        {
            Debug.Log("Enemy box collider componenet does not exist");
        }


    }

    private void SetNewRandomStartPosition()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), 8f, 0f);
    }

    void Update()
    {
        Move();
        CheckBounds();
    }

    private void CheckBounds()
    {
        if (!isDestroyed && transform.position.y < -6)
        {
            SetNewRandomStartPosition();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(Player.PLAYER_TAG))
        {
            Health health = other.transform.GetComponent<Health>();
            if (null != health)
            {
                health.Damage(1);
            }
            DestroyEnemy();
        }

        if (other.tag.Equals(Projectile.PROJECTILE_TAG))
        {
            Destroy(other.gameObject);
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        myAnimator.SetTrigger(TRIGGER_ON_ENEMY_DESTROYED);
        myBoxCollider.enabled = false;
        isDestroyed = true;

        Destroy(this.gameObject, 2.5f);
    }
}

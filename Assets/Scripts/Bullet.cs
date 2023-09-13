using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rb;

    private void Awake()
    {
        Destroy(this, 4f);
        _rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeBullet()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            GameManager.instance.AddScore(1);
            enemy.Die();
        }
        Destroy(gameObject);
    }
}

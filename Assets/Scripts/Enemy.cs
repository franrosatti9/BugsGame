using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform _playerPos;
    protected virtual void Start()
    {
        _playerPos = GameObject.Find("Player").transform;
    }
    
    void Update()
    {
        MoveTowardsPlayer();
    }

    // TODO: movimiento mas complejo, que esquive obstaculos y se mueva con estilo retro
    public void MoveTowardsPlayer()
    {
        transform.position += (_playerPos.position - transform.position).normalized * (Time.deltaTime * 2f);
        transform.up = (_playerPos.position - transform.position).normalized;
    }

    public virtual void Die()
    {
        EnemySpawner.instance.EnemyDied(null);
        Destroy(gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 3) // Layer del Player
        {
            other.GetComponent<HealthController>().GetDamage(10f);
        }
    }
}

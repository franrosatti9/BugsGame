using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : Enemy
{
    Bug bug;

    //Transform _playerPos;

    public Bug Bug => bug;

    protected override void Start()
    {
        base.Start();
        bug = GetComponent<Bug>();
        gameObject.SetActive(false);
    }
    
    public void Respawn(Vector3 newPosition)
    {
        transform.position = newPosition;
        gameObject.SetActive(true);
    }

    public override void Die()
    {
        EnemySpawner.instance.EnemyDied(bug);
        bug.RemoveBug();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    
    public void SpriteBecameVisible()
    {
        // Cuando el enemigo entra en la pantalla, se aplica el bug
        bug.ApplyBug();
    }
}

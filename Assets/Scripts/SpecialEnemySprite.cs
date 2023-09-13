using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemySprite : MonoBehaviour
{
    [SerializeField] SpecialEnemy _parent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        _parent.SpriteBecameVisible();
    }
}

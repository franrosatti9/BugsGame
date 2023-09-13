using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseMovementBug : Bug
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public override void ApplyBug()
    {
        base.ApplyBug();
        _player.inverseMovementBug = true;
    }

    public override void RemoveBug()
    {
        base.RemoveBug();
        _player.inverseMovementBug = false;
    }

    
}

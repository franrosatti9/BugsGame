using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDirectionBug : Bug
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyBug()
    {
        base.ApplyBug();
        _player.inverseShootingBug = true;
    }

    public override void RemoveBug()
    {
        base.RemoveBug();
        _player.inverseShootingBug = false;
    }
}

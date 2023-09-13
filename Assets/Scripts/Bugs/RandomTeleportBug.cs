using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleportBug : Bug
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
        _player.randomTeleportBug = true;
    }

    public override void RemoveBug()
    {
        base.RemoveBug();
        _player.randomTeleportBug = false;
    }
}
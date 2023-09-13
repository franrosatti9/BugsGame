using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseShootDash : Bug
{
    // Start is called before the first frame update
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
        _player.InvertShootAndDashButtons(true);
    }

    public override void RemoveBug()
    {
        base.RemoveBug();
        _player.InvertShootAndDashButtons(false);
    }
}

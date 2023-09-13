using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bug : MonoBehaviour
{
    protected Player _player;
    public string bugName;
    public string description;
    public Sprite icon;

    protected virtual void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }
    void Start()
    {
        
    }

    public virtual void ApplyBug()
    {
        GameManager.instance.AddBug();
    }
    public virtual void RemoveBug()
    {
        GameManager.instance.RemoveBug();
    }
}

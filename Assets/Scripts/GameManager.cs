using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] int maxBugsToLose;
    private int _currentBugs;

    public int MaxBugsToLose => maxBugsToLose;
    
    public event Action<int> OnCurrentBugsModified;
    public event Action<int> OnLoseGame;
    
    public static GameManager instance;

    int _score;
    public int Score => _score;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    void Start()
    {
        Time.timeScale = 1f;
        _currentBugs = 0;
    }

    public void AddBug()
    {
        _currentBugs++;
        OnCurrentBugsModified?.Invoke(_currentBugs);

        if(_currentBugs >= maxBugsToLose)
        {
            LoseGame();
        }
    }

    public void RemoveBug()
    {
        _currentBugs--;
        OnCurrentBugsModified?.Invoke(_currentBugs);
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        OnLoseGame?.Invoke(_score);
    }
}

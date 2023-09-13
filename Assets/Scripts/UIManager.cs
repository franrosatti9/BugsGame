using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bugsText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    
    private List<string> _currentBugsDescriptions = new List<string>();

    private void OnEnable()
    {
        EnemySpawner.instance.OnEnemySpawned += AddBugToList;
        EnemySpawner.instance.OnEnemyDied += EnemyDiedHandler;
        GameManager.instance.OnLoseGame += LoseHandler;
    }
    
    private void OnDisable()
    {
        EnemySpawner.instance.OnEnemySpawned -= AddBugToList;
        EnemySpawner.instance.OnEnemyDied -= EnemyDiedHandler;
        GameManager.instance.OnLoseGame -= LoseHandler;
    }
    void Start()
    {
        RefreshScore();
        RefreshBugList();
    }

    void AddBugToList(Bug newBug)
    {
        _currentBugsDescriptions.Add(newBug.description);
        RefreshBugList();
    }

    void EnemyDiedHandler(Bug bug)
    {
        RefreshScore();
        
        if(bug) RemoveBugFromList(bug);
    }

    void RemoveBugFromList(Bug bug)
    {
        if (_currentBugsDescriptions.Contains(bug.description))
        {
            Debug.Log("Removed");
            // Si lo removió
            _currentBugsDescriptions.Remove(bug.description);
            RefreshBugList();
        }
    }
    void RefreshBugList()
    {
        string newBugsText = "";
        for(int i = 0; i < _currentBugsDescriptions.Count; i++)
        {
            newBugsText += _currentBugsDescriptions[i] + "\n";
        }
        bugsText.text = newBugsText;
    }

    // TODO: Mover a un SceneManager
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // TODO: Mover a un SceneManager
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RefreshScore()
    {
        scoreText.text = GameManager.instance.Score.ToString();
    }

    void LoseHandler(int score)
    {
        finalScoreText.SetText("Score: " + score);
        loseCanvas.enabled = true;
    }
}

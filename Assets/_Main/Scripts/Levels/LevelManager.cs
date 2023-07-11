using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static event Action OnChangeLevel;
    public static event Action OnLevelComplete;
    public Level CurrentLevel => Levels[_currentLevel];
    public int StartLevel;
    
    [SerializeField] private List<Level> Levels = new List<Level>();
    [SerializeField] private PlayerTeleporter playerTeleporter;
    
    private int _currentLevel = 4; // -1 means that we are in the main menu
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
    }

    public void LevelComplete()
    {
        CurrentLevel.CompleteLevel();
        OnLevelComplete?.Invoke();
    }
    
    public void MoveToNextLevel()
    {
        MoveToLevel(_currentLevel + 1);
    }
    
    public void MoveToLevel(int levelNumber)
    {
        if (levelNumber >= Levels.Count)
            return;
        
        _currentLevel = levelNumber;

        var t = Levels[levelNumber].GetTeleportTransform();
        playerTeleporter.MoveToTransform(t);

        Levels[levelNumber].ShowInstructionScreen();

        OnChangeLevel?.Invoke();
    }
    
    public void RestartLevel()
    {
        CurrentLevel.RestartLevel();
    }
    
    public Level GetLevel(int levelNumber)
    {
        return Levels[levelNumber];
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
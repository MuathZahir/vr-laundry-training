using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static event Action OnChangeLevel;
    public static event Action OnLevelComplete;
    public Level CurrentLevel => Levels[_currentLevel];
    public int StartLevel;
    
    [SerializeField] private List<Level> Levels = new List<Level>();
    [SerializeField] private PlayerTeleporter playerTeleporter;
    
    private int _currentLevel = -1; // -1 means that we are in the main menu
    
    private void Awake()
    {
        Instance = this;

        OnChangeLevel = null;
        OnLevelComplete = null;
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
        
        if(_currentLevel != -1)
            CurrentLevel.LeaveLevel();
        
        _currentLevel = levelNumber;

        var t = Levels[levelNumber].GetTeleportTransform();
        playerTeleporter.MoveToTransform(t);

        CurrentLevel.EnterLevel();
        
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static event Action OnChangeLevel;
    public static event Action OnLevelComplete;
    public static event Action OnLevelRestart;
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
        OnLevelRestart = null;
    }

    [ContextMenu("Complete current level")]
    public void LevelComplete()
    {
        CurrentLevel.CompleteLevel();
        OnLevelComplete?.Invoke();
    }
    
    [ContextMenu("Move to next level")]
    public void MoveToNextLevel()
    {
        MoveToLevel(_currentLevel + 1);
    }
    
    // For testing purposes only
    [ContextMenu("Move to previous level")]
    private void MoveToPreviousLevel()
    {
        MoveToLevel(_currentLevel - 1);
    }
    
    public void MoveToLevel(int levelNumber)
    {
        if (levelNumber >= Levels.Count )
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
    
    [ContextMenu("Restart level")]
    public void RestartLevel()
    {
        CurrentLevel.RestartLevel();
        OnLevelRestart?.Invoke();
    }
    
    public Level GetLevel(int levelNumber)
    {
        return Levels[levelNumber];
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    [ContextMenu("Move to first incomplete level")]
    public void MoveToFirstIncompleteLevel()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            if (Levels[i].State == LevelState.NotCompleted)
            {
                MoveToLevel(i);
                return;
            }
        }
    }
}
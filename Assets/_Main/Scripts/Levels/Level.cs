using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public int ClothesRemaining { get; set; }
    public LevelState State { get; set; } = LevelState.NotStarted;
    public LevelInfo Info { get; private set; }
    public int LevelNumber { get; private set; }

    [SerializeField] private Transform clothesParent;
    [SerializeField] private int clothesRequired = 0;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private GameObject instructionScreen;
    [SerializeField] private GameObject menu;
    [SerializeField] private List<LevelRestartAction> levelRestartActions = new List<LevelRestartAction>();
    [SerializeField] private UnityEvent OnLevelComplete;
    
    private static readonly int Start = Animator.StringToHash("Start");
    private static readonly int Complete = Animator.StringToHash("End");

    private List<GameObject> _garments = new List<GameObject>();
    
    private void Awake()
    {
        ClothesRemaining = clothesRequired;
        LevelNumber = transform.GetSiblingIndex();
        Info = gameObject.GetComponent<LevelInfo>();
        
        foreach (Transform child in clothesParent)
        {
            _garments.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        
        menu.SetActive(false);
        instructionScreen.SetActive(false);
    }

    public void ShowInstructionScreen()
    {
        instructionScreen.SetActive(true);
    }

    public void EnterLevel()
    {
        if(State == LevelState.NotStarted)
            instructionScreen.SetActive(true);
        else
            StartLevel();
    }
    
    public void StartLevel()
    {
        ClothesActive(true);

        if(tutorial != null)
            tutorial.StartTutorial();
        
        menu.SetActive(true);
        
        if(State != LevelState.Completed)
            State = LevelState.NotCompleted;
    }

    public void LeaveLevel()
    {
        ClothesActive(false);
        menu.SetActive(false);
    }
    
    [ContextMenu("Complete Level")]
    public void CompleteLevel()
    {
        State = LevelState.Completed;
        levelCompleteUI.SetActive(true);
        OnLevelComplete?.Invoke();
    }

    public void RestartLevel()
    {
        foreach (var action in levelRestartActions)
        {
            action.PerformAction();
        }
        
        menu.SetActive(false);
        instructionScreen.SetActive(true);
        
        State = LevelState.NotStarted;
        ClothesRemaining = clothesRequired;
    }

    public Transform GetTeleportTransform()
    {
        return teleportPoint;
    }

    private void ClothesActive(bool active)
    {
        foreach (var garment in _garments)
        {
            garment.SetActive(active);
        }
    }
}

public enum LevelState
{
    NotStarted,
    NotCompleted,
    Completed
}
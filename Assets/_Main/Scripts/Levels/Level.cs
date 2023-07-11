using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int ClothesRemaining { get; set; }
    public LevelState State { get; set; } = LevelState.NotCompleted;
    public LevelInfo Info { get; private set; }
    public int LevelNumber { get; private set; }
    
    [SerializeField] private int clothesRequired = 0;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private GameObject instructionScreen;
    [SerializeField] private List<LevelRestartAction> levelRestartActions = new List<LevelRestartAction>();
    
    private Animator animator;
    
    private static readonly int Start = Animator.StringToHash("Start");
    private static readonly int Complete = Animator.StringToHash("End");

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        ClothesRemaining = clothesRequired;
        LevelNumber = transform.GetSiblingIndex();
        Info = gameObject.GetComponent<LevelInfo>();
    }

    public void ShowInstructionScreen()
    {
        instructionScreen.SetActive(true);
    }
    
    public void StartLevel()
    {
        animator.SetTrigger(Start);
        
        if(tutorial != null)
            tutorial.StartTutorial();
    }
    
    [ContextMenu("Complete Level")]
    public void CompleteLevel()
    {
        State = LevelState.Completed;
        animator.SetTrigger(Complete);
        levelCompleteUI.SetActive(true);
    }

    public void RestartLevel()
    {
        foreach (var action in levelRestartActions)
        {
            action.PerformAction();
        }
        
        State = LevelState.NotCompleted;
        ClothesRemaining = clothesRequired;
    }

    public Transform GetTeleportTransform()
    {
        return teleportPoint;
    }
}

public enum LevelState
{
    NotCompleted,
    Completed
}
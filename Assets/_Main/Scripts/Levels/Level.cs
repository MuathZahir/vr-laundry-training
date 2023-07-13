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

    [SerializeField] private Transform clothesParent;
    [SerializeField] private int clothesRequired = 0;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private GameObject instructionScreen;
    [SerializeField] private List<LevelRestartAction> levelRestartActions = new List<LevelRestartAction>();
    
    private Animator animator;
    
    private static readonly int Start = Animator.StringToHash("Start");
    private static readonly int Complete = Animator.StringToHash("End");

    private List<GameObject> _garments = new List<GameObject>();
    
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        ClothesRemaining = clothesRequired;
        LevelNumber = transform.GetSiblingIndex();
        Info = gameObject.GetComponent<LevelInfo>();
        
        foreach (Transform child in clothesParent)
        {
            _garments.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    public void ShowInstructionScreen()
    {
        instructionScreen.SetActive(true);
    }

    public void EnterLevel()
    {
        ClothesActive(true);
    }
    
    public void StartLevel()
    {
        animator.ResetTrigger(Complete);
        animator.SetTrigger(Start);

        if(tutorial != null)
            tutorial.StartTutorial();
    }

    public void LeaveLevel()
    {
        animator.ResetTrigger(Start);
        animator.SetTrigger(Complete);
        
        ClothesActive(false);
    }
    
    [ContextMenu("Complete Level")]
    public void CompleteLevel()
    {
        State = LevelState.Completed;
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
    NotCompleted,
    Completed
}
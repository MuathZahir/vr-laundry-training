using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.UI;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public static event Action<Level> OnLevelSelected;
    
    [SerializeField] private BubbleMenu menu;
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private GameObject currentLevelIndicator;
    [SerializeField] private GameObject completedLevelIndicator;
    
    private Level _level;

    public void InitializeButton()
    {
        UpdateButton();
    }

    public void SetLevel(Level level)
    {
        _level = level;
    }
    
    public void UpdateButton()
    {
        try
        {
            
            if (LevelManager.Instance.CurrentLevel == _level)
                currentLevelIndicator.SetActive(true);
            else
                currentLevelIndicator.SetActive(false);
            
            if (_level.State == LevelState.Completed)
                completedLevelIndicator.SetActive(true);
            else if (_level.State == LevelState.NotCompleted)
                completedLevelIndicator.SetActive(false);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    
    public void OnToggle(bool isOn)
    {
        if (!isOn || LevelManager.Instance.CurrentLevel == _level) return;
        
        OnLevelSelected?.Invoke(_level);
    }

    public void Enabled(bool isEnabled)
    {
    }
}

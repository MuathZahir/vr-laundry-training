using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public static event Action<Level> OnLevelSelected;

    [SerializeField] private BubbleButton button;
    [SerializeField] private BubbleMenu menu;
    
    private Image _background;
    private Level _level;

    public void InitializeButton()
    {
        _background = gameObject.GetComponent<Image>();
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
                _background.color = Color.blue; // TODO: this should change the bubble button not the background
            else if (_level.State == LevelState.Completed)
                _background.color = Color.green;
            else if (_level.State == LevelState.NotCompleted)
                _background.color = Color.grey;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    
    public void OnToggle(bool isOn)
    {
        if (menu.IsTutorial)
        {
            menu.ShowTutorialText();
            return;
        }
        
        if (!isOn || LevelManager.Instance.CurrentLevel == _level) return;
        
        OnLevelSelected?.Invoke(_level);
    }

    public void Enabled(bool isEnabled)
    {
        button.SetEnabled(isEnabled);
    }
}

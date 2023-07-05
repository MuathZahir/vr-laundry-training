using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public static event Action<Level> OnLevelSelected;
    
    private Toggle _toggle;
    private Image _background;
    private Level _level;

    public void InitializeButton()
    {
        _toggle = gameObject.GetComponent<Toggle>();
        _background = gameObject.GetComponent<Image>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggle);
    }
    
    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggle);
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
                _toggle.isOn = true;
            else if (_level.State == LevelState.Completed)
                _background.color = Color.green;
            else if (_level.State == LevelState.Tried)
                _background.color = Color.red;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    
    private void OnToggle(bool isOn)
    {
        if (!isOn || LevelManager.Instance.CurrentLevel == _level) return;
        
        OnLevelSelected?.Invoke(_level);
    }

    public void Enabled(bool isEnabled)
    {
        _toggle.interactable = isEnabled;
    }
}

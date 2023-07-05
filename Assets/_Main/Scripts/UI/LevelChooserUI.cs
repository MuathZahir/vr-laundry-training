using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChooserUI : Page
{
    [SerializeField] private LevelButton[] levelButtons;
    
    private void Start()
    {
        LevelButton.OnLevelSelected += OnLevelButtonClicked;
    }

    public override void Initialize()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetLevel(LevelManager.Instance.GetLevel(i));
            levelButtons[i].InitializeButton();
        }
    }

    public override void OnLevelLoaded()
    {
        foreach (var button in levelButtons)
        {
            button.UpdateButton();
        }
    }
    
    private void OnLevelButtonClicked(Level level)
    {
        LevelManager.Instance.MoveToLevel(level.LevelNumber);
    }

    public void Enabled(bool isEnabled)
    {
        foreach (var button in levelButtons)
        {
            button.Enabled(isEnabled);
        }
    }
}

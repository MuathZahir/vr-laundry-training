using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoUI : Page
{
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;
    [SerializeField] private Image levelImage;
    
    private LevelInfo _levelInfo;

    public override void Initialize()
    {
    }
    
    private void SetLevelInfo(LevelInfo levelInfo)
    {
        _levelInfo = levelInfo;
        levelNameText.text = _levelInfo.levelName;
        levelDescriptionText.text = _levelInfo.levelDescription;
        
        levelImage.gameObject.SetActive(true);
        levelImage.sprite = _levelInfo.levelImage;
    }

    public override void OnLevelLoaded()
    {
        SetLevelInfo(LevelManager.Instance.CurrentLevel.Info);
    }

    public void SetTutorialText()
    {
        levelNameText.text = "Tutorial";
        levelDescriptionText.text = "Finish tutorial";
        levelImage.gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class IncompleteUI : MonoBehaviour
{
    [SerializeField] private Image[] completeIndicators;
    [SerializeField] private Color incompleteColor;
    [SerializeField] private Sprite incompleteSprite;
    [SerializeField] private Color completeColor;
    [SerializeField] private Sprite completeSprite;
    [SerializeField] private GameObject completeUI;
    
    private void OnEnable()
    {
        LevelManager.OnLevelComplete += UpdateUI;
        LevelManager.OnLevelRestart += UpdateUI;
    }
    
    private void OnDisable()
    {
        LevelManager.OnLevelComplete -= UpdateUI;
        LevelManager.OnLevelRestart -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var isAllComplete = true;

        for (int i = 0; i < completeIndicators.Length; i++)
        {
            var isComplete = LevelManager.Instance.GetLevel(i).State == LevelState.Completed;

            if (!isComplete)
                isAllComplete = false;

            completeIndicators[i].sprite = isComplete ? completeSprite : incompleteSprite;
            completeIndicators[i].color = isComplete ? completeColor : incompleteColor;
        }

        completeUI.SetActive(isAllComplete);
        gameObject.SetActive(!isAllComplete);
    }
}

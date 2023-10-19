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

    [SerializeField] private GameObject root;
    [SerializeField] private GameObject completeUI;
    [SerializeField] private GameObject incompleteUI;
    
    private bool _isAllComplete = false;
    
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelComplete += UpdateUI;
        LevelManager.Instance.OnLevelRestart += UpdateUI;
    }
    
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelComplete -= UpdateUI;
        LevelManager.Instance.OnLevelRestart -= UpdateUI;
    }

    private void Start()
    {
    }

    public void UpdateUI()
    {
        _isAllComplete = true;

        for (int i = 0; i < completeIndicators.Length; i++)
        {
            var isComplete = LevelManager.Instance.GetLevel(i).State == LevelState.Completed;

            if (!isComplete)
                _isAllComplete = false;

            completeIndicators[i].sprite = isComplete ? completeSprite : incompleteSprite;
            completeIndicators[i].color = isComplete ? completeColor : incompleteColor;
        }
        
        completeUI.SetActive(_isAllComplete);
        incompleteUI.SetActive(!_isAllComplete);
    }

    public void ShowUI()
    {
        root.SetActive(true);
    }
    
    public void HideUI()
    {
        root.SetActive(false);
    }
    
}

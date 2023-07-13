using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.UI;
using UnityEngine;

public class BubbleMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private LevelChooserUI levelChooser;
    [SerializeField] private LevelInfoUI infoPage;
    [SerializeField] private PagesUI pages;

    [SerializeField] private GameObject tutorialText;
    
    private BubbleButton _currentlySelectedButton;

    public bool IsTutorial { get; set; } = true;

    private void Start()
    {
        pages.Initialize();
        menu.SetActive(false);
        
        // Make sure that the pages don't work in tutorial mode
        //levelChooser.Enabled(false);
        infoPage.SetTutorialText();
    }

    public void TutorialComplete()
    {
        IsTutorial = false;
        levelChooser.Enabled(true);
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        
        infoPage.gameObject.SetActive(false);
        levelChooser.gameObject.SetActive(false);
        menuButtons.SetActive(true);
    }
    
    public void CloseMenu()
    {
        _currentlySelectedButton = null;
        menu.SetActive(false);
    }
    
    public void OnChooseButtonPose()
    {
        CloseMenu();
    }
    
    public void SetSelectedButton(BubbleButton button)
    {
        if (_currentlySelectedButton != null)
        {
            _currentlySelectedButton.Reset();
        }
        
        _currentlySelectedButton = button;
    }

    public void MoveToNextLevel()
    {
        if (!IsTutorial)
        {
            LevelManager.Instance.MoveToNextLevel();
        }
        else
        {
            ShowTutorialText();
        }
    }

    public void RestartLevel()
    {
        if (!IsTutorial)
        {
            LevelManager.Instance.RestartLevel();
        }
        else
        {
            ShowTutorialText();
        }
    }

    public void ShowTutorialText()
    {            
        StartCoroutine(ShowTutorialText_Coroutine());
    }
    
    private IEnumerator ShowTutorialText_Coroutine()
    {
        tutorialText.SetActive(true);
        yield return new WaitForSeconds(1f);
        tutorialText.SetActive(false);
    }
}

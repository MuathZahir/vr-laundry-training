using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.UI;
using UnityEngine;

public class BubbleMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private PagesUI pages;

    private BubbleButton _currentlySelectedButton;

    private void Start()
    {
        pages.Initialize();
    }

    private void OnEnable()
    {
        menuButtons.SetActive(true);
        pages.Reset();
    }

    public void MoveToNextLevel()
    {
        LevelManager.Instance.MoveToNextLevel();
    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();
    }

}

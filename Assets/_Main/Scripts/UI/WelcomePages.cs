using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePages : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private BubbleMenu bubbleMenu;
    
    private int _currentPageIndex = 0;
    
    private void Start()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        
        pages[_currentPageIndex].SetActive(true);
    }
    
    public void NextPage()
    {
        pages[_currentPageIndex].SetActive(false);
        _currentPageIndex++;
        pages[_currentPageIndex].SetActive(true);
    }
    
    public void StartSimulation()
    {
        LevelManager.Instance.MoveToLevel(LevelManager.Instance.StartLevel);
    }
    
}

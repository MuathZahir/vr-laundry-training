using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesUI : MonoBehaviour
{
    private readonly List<Page> _pages = new List<Page>();
    
    public void Initialize()
    {
        foreach (Transform page in transform)
        {
            if (!page.TryGetComponent<Page>(out var pageComponent)) continue;
            
            _pages.Add(pageComponent);
            page.gameObject.SetActive(false);
            
            pageComponent.Initialize();
        }

        LevelManager.OnChangeLevel += OnLevelLoaded;
        LevelManager.OnLevelComplete += OnLevelComplete;
        LevelManager.OnLevelRestart += OnLevelRestart;
    }

    private void OnLevelRestart()
    {
        foreach (var page in _pages)
        {
            page.OnLevelRestart();
        }
    }

    private void OnLevelComplete()
    {
        foreach (var page in _pages)
        {
            page.OnLevelComplete();
        }
    }

    private void OnLevelLoaded()
    {
        foreach (var page in _pages)
        {
            page.OnLevelLoaded();
        }
    }

    public void Reset()
    {
        foreach (var page in _pages)
        {
            page.gameObject.SetActive(false);
        }
    }
}

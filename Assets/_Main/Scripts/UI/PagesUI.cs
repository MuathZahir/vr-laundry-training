using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesUI : MonoBehaviour
{
    private readonly List<Page> _pages = new List<Page>();
    
    private void Start()
    {
        foreach (Transform page in transform)
        {
            if (!page.TryGetComponent<Page>(out var pageComponent)) continue;
            
            _pages.Add(pageComponent);
            page.gameObject.SetActive(false);
            
            pageComponent.Initialize();
        }

        LevelManager.OnChangeLevel += OnLevelLoaded;
    }
    
    private void OnLevelLoaded()
    {
        foreach (var page in _pages)
        {
            page.OnLevelLoaded();
        }
    }
}

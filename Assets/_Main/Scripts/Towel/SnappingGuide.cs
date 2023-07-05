using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingGuide : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color hoverColor;
    
    [ColorUsage(true, true)]
    [SerializeField] private Color correctColor;
    
    [ColorUsage(true, true)]
    [SerializeField] private Color wrongColor;
    
    private MeshRenderer _meshRenderer;
    
    private SnappingGuideState _state;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    
    public void SetState(SnappingGuideState state)
    {
        var color = Color.white;
        
        switch (state)
        {
            case SnappingGuideState.Hover:
                color = hoverColor;
                break;
            case SnappingGuideState.Correct:
                color = correctColor;
                break;
            case SnappingGuideState.Wrong:
                color = wrongColor;
                break;
        }
        
        _meshRenderer.material.SetColor("_Color", color);
    }
}

public enum SnappingGuideState
{
    Hover,
    Correct,
    Wrong,
}

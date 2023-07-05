using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EyeInteractables : MonoBehaviour
{
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material defaultMaterial;
    
    [SerializeField] private UnityEvent onSelect;
    [SerializeField] private UnityEvent onDeselect;
    
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    
    public void Select()
    {
        _meshRenderer.material = selectedMaterial;
        onSelect.Invoke();
    }
    
    public void Deselect()
    {
        _meshRenderer.material = defaultMaterial;
        onDeselect.Invoke();
    }
}

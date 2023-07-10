using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EyeInteractables : MonoBehaviour
{
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private float gazeTime = 2f;
    
    [ColorUsage(true, true)]
    [SerializeField] private Color startColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color endColor;

    [SerializeField] private UnityEvent onGaze;
    [SerializeField] private UnityEvent onSelect;
    [SerializeField] private UnityEvent onStopGaze;
    
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    
    public void Select()
    {
        _meshRenderer.material = selectedMaterial;

        StartCoroutine(StartGaze());
        
        onGaze.Invoke();
    }
    
    private IEnumerator StartGaze()
    {
        var timer = 0f;
        var material = _meshRenderer.material;
        var color = startColor;
        
        while (timer < gazeTime)
        {
            timer += Time.deltaTime;
            color = Color.Lerp(startColor, endColor, timer / gazeTime);
            material.color = color;
            yield return null;
        }
        
        onSelect.Invoke();
    }
    
    public void Deselect()
    {
        _meshRenderer.material = defaultMaterial;
        StopAllCoroutines();
        onStopGaze.Invoke();
    }
}

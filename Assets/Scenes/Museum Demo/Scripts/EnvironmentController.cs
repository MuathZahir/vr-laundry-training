using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    private GameObject _environment;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private AudioSource audioSource;
    
    void Start()
    {
        _environment = transform.GetChild(0).gameObject;
    }
    
    public void StartAnimations()
    {
        audioSource.Play();
        
        foreach (var animator in animators)
        {
            animator.enabled = false;
            animator.enabled = true;
        }
    }
    
    public void StopAnimations()
    {
        audioSource.Stop();
        
        foreach (var animator in animators)
        {
            animator.enabled = false;
        }
    }
}

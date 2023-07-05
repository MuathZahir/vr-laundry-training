using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private List<Animator> animators;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Lighter>(out Lighter l) && l.IsLit)
        {
            fireParticles.Play();

            foreach (var animator in animators)
            {
                animator.enabled = true;
            }
        }
    }
}

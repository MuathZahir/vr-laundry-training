using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMover : MonoBehaviour
{
    private Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.Play("FoodMove", -1, Random.Range(0f, 1f));
    }
}

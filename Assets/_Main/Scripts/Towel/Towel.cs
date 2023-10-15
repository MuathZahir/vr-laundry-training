using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UCloth;
using UnityEngine;
using UnityEngine.Events;

public class Towel : MonoBehaviour
{
    public event Action OnPlacedTowel = null;
    
    [HideInInspector] public List<TowelFixedPoint> FixedPoints = new List<TowelFixedPoint>();
    
    [SerializeField] private Transform fixedPoints;
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private Mesh mesh;
    [SerializeField] private GameObject foldedTowelTemplate;
    
    [SerializeField] private UnityEvent onFoldedFirstTime;
    [SerializeField] private UnityEvent onFoldedSecondTime;

    public UCCloth Cloth { get; private set; }
    public bool IsFolded { get; set; } = false;

    private void Awake()
    {
        Cloth = gameObject.GetComponent<UCCloth>();
    }

    public void CheckIfFolded()
    {
        switch (FixedPoints.Count)
        {
            // Folded first time
            case 4:
                tutorial.MoveToNextStep();
                onFoldedFirstTime?.Invoke();
                
                break;
            // Folded second time
            case 2:
                IsFolded = true;
                tutorial.CompleteTutorial();
                
                Instantiate(foldedTowelTemplate, foldedTowelTemplate.transform.position,
                    foldedTowelTemplate.transform.rotation, transform.parent).SetActive(true);
                
                onFoldedSecondTime?.Invoke();
                
                Destroy(gameObject);
                
                break;
        }
    }

    public void OnPlaced()
    {
        OnPlacedTowel?.Invoke();
    }
    
    [ContextMenu("Reset Towel")]
    public void ResetTowel()
    {
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        IsFolded = false;
        FixedPoints.Clear();
    }

}

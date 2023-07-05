using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class MatchBox : MonoBehaviour
{
    [SerializeField] private Match matchPrefab;
    [SerializeField] private Transform matchPosition;
    [SerializeField] private HandGrabInteractor interactor;

    private void Awake()
    {
    }

    public void OnMatchGrab()
    {
        Match m = Instantiate(matchPrefab, matchPosition.position, matchPosition.rotation, matchPosition);
        //interactor.ForceRelease();
        //interactor.ForceSelect(m.GetComponentsInChildren<HandGrabInteractable>()[0]);
        //interactor.Unselect();
        interactor.ForceSelect(m.GetComponentsInChildren<HandGrabInteractable>()[0]);
        //interactor.SetComputeShouldUnselectOverride(() => !ReferenceEquals(interactor.Interactable, interactor.SelectedInteractable), true);
        interactor.ClearComputeShouldUnselectOverride();
    }
}

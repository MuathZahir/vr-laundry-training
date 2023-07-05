using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchBoxTransformer : MonoBehaviour, ITransformer
{
    [SerializeField] private Match matchPrefab;
    private IGrabbable _grabbable;
    private Match _instantiatedMatch;

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable;
    }

    public void BeginTransform()
    {
        var grabPoint = _grabbable.GrabPoints[1];
        _instantiatedMatch = Instantiate(matchPrefab, grabPoint.position, grabPoint.rotation);
    }

    public void UpdateTransform()
    {
        var transform1 = _instantiatedMatch.transform;

        _instantiatedMatch.transform.GetComponent<Grabbable>().GrabPoints[0] = _grabbable.GrabPoints[1];
        transform.GetComponent<IInteractor>().Select();
    }

    public void EndTransform()
    {
    }
}

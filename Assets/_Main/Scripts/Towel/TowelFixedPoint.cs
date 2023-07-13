using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCloth;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TowelFixedPoint : MonoBehaviour
{
    public event Action OnPutDown = null;
    
    [SerializeField] private Towel towel;
    [SerializeField] private UCCloth cloth;
    [SerializeField] private SnappingGuide snappingGuide;
    [SerializeField] private float snappingDistance = 0.1f;
    [SerializeField] private List<TowelFixedPoint> snappingPoints = new List<TowelFixedPoint>();

    private bool _isGrabbed = false;
    
    private BoxCollider _collider;
    private Rigidbody _rigidbody;

    private TowelFixedPoint _snapTarget;
    
    private void Start()
    {
        towel.FixedPoints.Add(this);
        
        _collider = gameObject.GetComponent<BoxCollider>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // private void Start()
    // {
    //     towel.OnSimulationFinished += SetFollowPoint;
    // }
    //
    // private void SetFollowPoint(object sender, EventArgs eventArgs)
    // {
    //     StartCoroutine(QueryPoints(transform.position, 0, result =>
    //     {
    //         _followPoint = result[0];
    //         _ready = true;
    //     }));
    //     
    //     towel.OnSimulationFinished -= SetFollowPoint;
    // }

    private void Update()
    {
        if(!_isGrabbed) return;

        TowelFixedPoint closestPoint = null;
        var closestDistance = float.MaxValue;
        
        foreach (var point in towel.FixedPoints)
        {
            if(point._isGrabbed) continue;
            
            var distance = Vector3.SqrMagnitude(point.transform.position - transform.position);
            if (distance < closestDistance)
            {
                closestPoint = point;
                closestDistance = distance;
            }
        }
        
        if(closestDistance < snappingDistance)
        {
            snappingGuide.gameObject.SetActive(true);
            snappingGuide.transform.position = closestPoint.transform.position;
            snappingGuide.transform.SetParent(closestPoint.transform);
            
            var snappingGuideState = CanSnapTo(closestPoint) ? SnappingGuideState.Correct : SnappingGuideState.Wrong;
            snappingGuide.SetState(snappingGuideState);
            
            _snapTarget = closestPoint;
            return;
        }
        
        snappingGuide.gameObject.SetActive(false);
        _snapTarget = null;
    }

    public void OnGrab()
    {
        _isGrabbed = true;
        Debug.Log("Grabbing " + name);
    }

    public void OnRelease()
    {
        _isGrabbed = false;
        snappingGuide.transform.position = transform.position;
        snappingGuide.gameObject.SetActive(false);
        
        if(_snapTarget != null && CanSnapTo(_snapTarget))
            SnapTo(_snapTarget);
    }
    
    public void OnHover()
    {
        if(_isGrabbed) return;
        
        snappingGuide.gameObject.SetActive(true);
        snappingGuide.transform.position = transform.position;
        snappingGuide.transform.SetParent(transform);
        snappingGuide.SetState(SnappingGuideState.Hover);
    }

    private void SnapTo(TowelFixedPoint snapTarget)
    {
        snapTarget._collider.enabled = false;
        var r = snapTarget._rigidbody;

        r.isKinematic = true;
        r.useGravity = false;
        r.velocity = Vector3.zero;

        transform.position = snapTarget.transform.position;
        snapTarget.transform.SetParent(transform);
        
        snappingPoints.Remove(snapTarget);
        towel.FixedPoints.Remove(snapTarget);
        snapTarget.gameObject.SetActive(false);
        
        towel.CheckIfFolded();
    }

    private bool CanSnapTo(TowelFixedPoint snapTarget)
    {
        if (snappingPoints.Count == 0)
            return false;
        
        return snappingPoints[0] == snapTarget;
    }
    
    private IEnumerator QueryPoints(float3 pos, float radius, Action<List<ushort>> finishCallback)
    {
        // Create a TaskCompletionSource to await the completion of the async function
        var tcs = new TaskCompletionSource<List<ushort>>();

        // Call the async function
        var task = cloth.QueryClosestPoints(new UCPointQueryData(pos, radius));
        Debug.LogWarning("Sent Query");

        // Handle the completion of the task
        task.ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                tcs.SetException(t.Exception);
            }
            else if (t.IsCanceled)
            {
                tcs.SetCanceled();
            }
            else
            {
                tcs.SetResult(t.Result);
            }
        });
        
        Debug.LogWarning("Waiting for Query");

        // Wait for the task to complete
        while (!tcs.Task.IsCompleted)
        {
            yield return null;
        }

        // Access the result of the async function
        finishCallback(tcs.Task.Result);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<TowelGatherer>(out var gatherer) && !_isGrabbed && towel.IsFolded)
        {
            towel.FixedPoints.Remove(this);

            if (towel.FixedPoints.Count == 0)
            {
                cloth.enabled = false;
                towel.OnPlaced();
            }
            
            gameObject.SetActive(false);
        }
    }
}

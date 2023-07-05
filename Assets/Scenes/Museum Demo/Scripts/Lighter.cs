using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Lighter : MonoBehaviour, IHandGrabUseDelegate
{
    public bool IsLit { get; private set; }
    
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Transform lighterLid;
    
    private float _lastUseTime;
    private float _dampedUseStrength = 0f;
    private float _triggerSpeed = 3f;
    private AnimationCurve _strengthCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
    private float _fireThresold = 0.7f;
    private float _releaseThresold = 0.3f;
    private bool _wasFired = false;

    public void BeginUse()
    {
        _dampedUseStrength = 0f;
        _lastUseTime = Time.realtimeSinceStartup;
    }

    public void EndUse()
    {

    }

    public float ComputeUseStrength(float strength)
    {
        float delta = Time.realtimeSinceStartup - _lastUseTime;
        _lastUseTime = Time.realtimeSinceStartup;
        if (strength > _dampedUseStrength)
        {
            _dampedUseStrength = Mathf.Lerp(_dampedUseStrength, strength, _triggerSpeed * delta);
        }
        else
        {
            _dampedUseStrength = strength;
        }
        float progress = _strengthCurve.Evaluate(_dampedUseStrength);
        UpdateTriggerProgress(progress);
        return progress;
    }

    private void UpdateTriggerProgress(float progress)
    {
        Debug.Log(lighterLid.rotation.eulerAngles.x + " " + lighterLid.localRotation.eulerAngles.x);
        if (progress >= _fireThresold && !_wasFired && (lighterLid.localRotation.eulerAngles.x > 330f || lighterLid.localRotation.eulerAngles.x < 100f))
        {
            _wasFired = true;
            fireParticles.Play();
            IsLit = true;
        }
        else if (progress <= _releaseThresold)
        {
            _wasFired = false;
            fireParticles.Stop();
            IsLit = false;
        }
    }
}

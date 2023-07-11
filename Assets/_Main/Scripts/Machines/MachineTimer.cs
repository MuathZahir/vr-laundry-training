using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MachineTimer : MonoBehaviour
{
    public event Action OnTimerStart = null;
    public event Action OnTimerStop = null;
    public event Action OnTimerDone = null;
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int secondsBeforeSkip = 5;
    [SerializeField] private float skipTimerSpeed = 100f;
    
    private int _timer = 60; // in seconds
    private int _secondsBeforeSkip;

    private void SetTimer(int timeInSeconds)
    {
        _timer = timeInSeconds;
        _secondsBeforeSkip = secondsBeforeSkip;
        timerText.text = FormatTimerText();
    }
    
    private string FormatTimerText()
    {
        var minutes = Mathf.Floor(_timer / 60).ToString("00");
        var seconds = (_timer % 60).ToString("00");
        
        return $"{minutes}:{seconds}";
    }
    
    public void StartTimer(int timeInSeconds)
    {
        SetTimer(timeInSeconds);
        
        StartCoroutine(Timer());
        OnTimerStart?.Invoke();
    }
    
    private IEnumerator Timer()
    {
        while (_timer > 0 && _secondsBeforeSkip > 0)
        {
            timerText.text = FormatTimerText();
            yield return new WaitForSeconds(1);
            _timer--;
            _secondsBeforeSkip--;
        }
        
        while (_timer > 0)
        {
            timerText.text = FormatTimerText();
            yield return null;
            _timer -= Mathf.RoundToInt(Time.deltaTime * skipTimerSpeed);
        }
        
        timerText.text = "Done!";
        OnTimerDone?.Invoke();
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        OnTimerStop?.Invoke();
    }
}

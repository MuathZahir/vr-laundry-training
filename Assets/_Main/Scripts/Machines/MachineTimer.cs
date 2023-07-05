using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MachineTimer : MonoBehaviour
{
    public event Action OnTimerStart = null;
    public event Action OnTimerDone = null;
    
    [SerializeField] private int secondIncrements = 30;
    [SerializeField] private TextMeshProUGUI timerText;
    
    private int _timer = 60; // in seconds

    private void SetTimer(int timeInSeconds)
    {
        _timer = timeInSeconds;
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
        while (_timer > 0)
        {
            timerText.text = FormatTimerText();
            yield return new WaitForSeconds(1);
            _timer--;
        }
        
        timerText.text = "Done!";
        OnTimerDone?.Invoke();
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        OnTimerDone?.Invoke();
    }
}

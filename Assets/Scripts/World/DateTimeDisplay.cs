using System;
using TMPro;
using UnityEngine;

public class DateTimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayClockText;
    
    [SerializeField] private TextMeshProUGUI _totalDaysPassedText;
    
    [SerializeField] private TextMeshProUGUI _currentDayPart;

    [SerializeField] private DayAndNightCycle _dayAndNightCycle;

    private void ChangeClockTimeText(DateTime currentTime)
    {
        if (_dayClockText != null)
        {
            _dayClockText.text = currentTime.ToString("HH:mm");
        }
    }

    private void ChangeTotalPassedDaysText(int totalDaysPassed)
    {
        _totalDaysPassedText.text = "Total Days Passed: " + totalDaysPassed;
    }
    
    private void ChangeCurrentDayPart(string datePart)
    {
        _currentDayPart.text = datePart;
    }
    
    private void OnEnable()
    {
        _dayAndNightCycle.TimeWasChanged += ChangeClockTimeText;
        _dayAndNightCycle.PassedDaysAmountChanged += ChangeTotalPassedDaysText;
        _dayAndNightCycle.CurrentDayPartChanged += ChangeCurrentDayPart;
    }
    private void OnDisable()
    {
        _dayAndNightCycle.TimeWasChanged -= ChangeClockTimeText;
        _dayAndNightCycle.PassedDaysAmountChanged -= ChangeTotalPassedDaysText;
        _dayAndNightCycle.CurrentDayPartChanged -= ChangeCurrentDayPart;
    }
}

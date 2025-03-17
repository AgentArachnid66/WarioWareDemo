using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerUI : MonoBehaviour
{
    public TimerObject timer;
    private Label timeRemaining = null;

    private void OnEnable()
    {
        UIDocument document = GetComponent<UIDocument>();
        timeRemaining = document.rootVisualElement.Q<Label>("TimeRemaining");
        timer.PostTimeUpdated.AddListener(OnTimeUpdated);
        timer.PostTimerCompleted.AddListener(OnTimeCompleted);
    }

    public void OnTimeUpdated(int Seconds)
    {
        if (timeRemaining != null)
        {
            timeRemaining.text = "Time Remaining: " + Seconds;
        }
    }

    public void OnTimeCompleted()
    {
        if (timeRemaining != null)
        {
            timeRemaining.text = "Times Up";
        }
    }
}

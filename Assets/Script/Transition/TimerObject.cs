using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Timer", menuName = "ScriptableObjects/Transition Conditions/Timer", order = 1)]
public class TimerObject : TransitionCondition
{
    public int DefaultTime = 10;

    public UnityEvent<int> PostTimeUpdated = new UnityEvent<int>();
    public UnityEvent PostTimerCompleted = new UnityEvent();

    private bool _finished = false;
    public override IEnumerator ExecuteCondition()
    {
        _finished = false;
        int counter = DefaultTime;
        PostTimeUpdated.Invoke(counter);
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            PostTimeUpdated.Invoke(counter);
            Debug.Log($"Timer Updated: {counter}");
        }
        _finished = true;
        PostTimerCompleted.Invoke();
        Debug.Log($"Timer Completed");
        OnConditionMet.Invoke(this);
    }

    public override bool IsConditionMet()
    {
        return _finished;
    }
}

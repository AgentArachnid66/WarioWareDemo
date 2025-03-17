using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class TransitionCondition : ScriptableObject
{
    public abstract IEnumerator ExecuteCondition();

    public UnityEvent<TransitionCondition> OnConditionMet = new UnityEvent<TransitionCondition>();

    public abstract bool IsConditionMet();
}

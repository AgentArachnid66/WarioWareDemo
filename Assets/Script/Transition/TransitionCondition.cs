using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class TransitionCondition : MonoBehaviour
{
    public abstract IEnumerator ExecuteCondition();

    public UnityEvent<TransitionCondition> OnConditionMet = new UnityEvent<TransitionCondition>();

    public abstract bool IsConditionMet();
}

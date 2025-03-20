using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionManager : MonoBehaviour
{

    [SerializeField]
    List<TransitionCondition> transitionConditions = new List<TransitionCondition>();

    public UnityAction OnAllConditionsMet;

    private void OnEnable()
    {
        for(int i = 0; i < transitionConditions.Count; i++)
        {
            transitionConditions[i].OnConditionMet.AddListener(OnConditionMet);
        }
    }

    private void OnDisable()
    {
        foreach (TransitionCondition condition in transitionConditions)
        {
            condition.OnConditionMet.RemoveListener(OnConditionMet);
        }
    }

    private void OnConditionMet(TransitionCondition Condition)
    {
        int Index = transitionConditions.FindIndex(condition => condition == Condition);
        if(Index > -1 && Index < transitionConditions.Count)
        {
            Debug.Log($"Condition at Index {Index} has been met");
        }

        for(int i = 0; i < transitionConditions.Count; i++)
        {
            if (!transitionConditions[i].IsConditionMet())
            {
                return;
            }
        }
        if (OnAllConditionsMet.GetInvocationList().Length > 0)
        {
            OnAllConditionsMet.Invoke();
        }
    }

    [ContextMenu("Execute Conditions")]
    public void ExecuteAllConditions()
    {
        for(int i = 0; i < transitionConditions.Count; i++)
        {
            StartCoroutine(transitionConditions[i].ExecuteCondition());
        }
    }
}

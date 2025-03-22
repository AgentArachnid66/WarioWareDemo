using System.Collections;
using UnityEngine;

public class TestSceneLogic : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LogicCoroutine());
    }

    private IEnumerator LogicCoroutine()
    {
        float waitDuration = 5f;
        for (float timeElapsed = 0; timeElapsed < waitDuration; timeElapsed += Time.deltaTime)
        {
            Debug.Log($"Time: {timeElapsed}");
            yield return null;
        }
        
        if (Input.GetKey(KeyCode.LeftShift)) MicroGameManager.Instance.Failure.Invoke();
        else MicroGameManager.Instance.Success.Invoke();
    }
}

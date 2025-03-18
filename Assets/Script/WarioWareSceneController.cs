using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WarioWareSceneController : MonoBehaviour
{

    public List<string> ScenePaths = new List<string>();

    [SerializeField]
    private int _ActiveSceneIndex = -1;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode LoadMode)
    {
        Debug.Log($"Scene Loaded {scene.name}");
        TransitionManager TransitionObject = GameObject.FindFirstObjectByType<TransitionManager>();
        if(TransitionObject != null)
        {
            TransitionObject.OnAllConditionsMet += OnTransitionConditionsMet;

            TransitionObject.ExecuteAllConditions();
        }
    }

    private void OnTransitionConditionsMet()
    {
        Debug.Log("Transitioning back to Main Menu");
        UnloadActiveWarioScene();
    }

    void LoadWarioSceneAtIndex(int Index)
    {
        if(ScenePaths.Count > Index && Index > -1)
        {
            SceneManager.LoadSceneAsync(ScenePaths[Index], LoadSceneMode.Additive);
            _ActiveSceneIndex = Index;
        }
    }

    void UnloadActiveWarioScene()
    {
        if (ScenePaths.Count > _ActiveSceneIndex && _ActiveSceneIndex > -1)
        {
            SceneManager.UnloadSceneAsync(_ActiveSceneIndex);
            _ActiveSceneIndex =  -1;

            StartCoroutine(RepeatLevelSelection());
        }

    }

    private void Start()
    {
        int RandomIndex = Random.Range(0, ScenePaths.Count);
        LoadWarioSceneAtIndex(RandomIndex);
    }

    private IEnumerator RepeatLevelSelection()
    {
        yield return new WaitForSeconds(2);

        int RandomIndex = Random.Range(0, ScenePaths.Count);
        LoadWarioSceneAtIndex(RandomIndex);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MicroGameManager : MonoBehaviour
{
    #region Instance 
    // if the instance is not equal to null, return instance.
    // if instance is equal to null find MicroGameManager and return it.
    public static MicroGameManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            _instance = FindAnyObjectByType<MicroGameManager>();
            return _instance;
        }
        // if instance is equal to null, sets instance equal to the value.
        protected set
        {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }

    
    private static MicroGameManager _instance;
    #endregion

    #region Scenes 
    [SerializeField] private string _gameOverScene;
    public string[] ScenePaths;

    public int[] SceneAccessCount;
    #endregion

    #region Actions 
    public Action Success;
    public Action Failure;
    #endregion

    #region Properties
    private string _activeScene;
    #endregion


    [SerializeField]
    private int _ActiveSceneIndex = -1;


    void LoadWarioSceneAtIndex(int Index)
    {
        // Index ^ is parameter. Index below is an argument
        // Checking if the index is within the ScenePaths.
        if (ScenePaths.Length > Index && Index > -1)
        {
            // SceneManager is loading the scene with the index and adding the scene we are loading, to the scene.
            SceneManager.LoadSceneAsync(ScenePaths[Index], LoadSceneMode.Additive);
            //keeps track of active scene
            _ActiveSceneIndex = Index;
            SceneAccessCount[Index]++;
        }
    }

    void UnloadActiveWarioScene()
    {
        // Checking if ____ its within the scene paths
        if (ScenePaths.Length > _ActiveSceneIndex && _ActiveSceneIndex > -1)
        {
            SceneManager.UnloadSceneAsync(ScenePaths[_ActiveSceneIndex]);
            _ActiveSceneIndex = -1;

            StartCoroutine(RepeatLevelSelection());
        }

    }

    private void Start()
    {
        int RandomIndex = Random.Range(0, ScenePaths.Length);
        LoadWarioSceneAtIndex(RandomIndex);

    }

    private IEnumerator RepeatLevelSelection()
    {
        yield return new WaitForSeconds(2);

        int RandomIndex = Random.Range(0, ScenePaths.Length);
        LoadWarioSceneAtIndex(RandomIndex);
    }

    private void Awake()
    {
        Success += OnMicroGameSuccess;
        Failure += OnMicroGameFailure;
    }

    private void OnMicroGameFailure()
    {
        // Checking if ____ its within the scene paths
        if (ScenePaths.Length > _ActiveSceneIndex && _ActiveSceneIndex > -1)
        {
            SceneManager.UnloadSceneAsync(ScenePaths[_ActiveSceneIndex]);
            _ActiveSceneIndex = -1;

            SceneManager.LoadSceneAsync(_gameOverScene, LoadSceneMode.Additive);

        }
    }
    // On success its going to unload the scene
    private void OnMicroGameSuccess()
    {
        UnloadActiveWarioScene();
    }



    /*
     * Use the code below to retrieve how many times a scene has been loaded. Replace Pilot with the name of the scene that
     * you are trying to retrieve the load count
    int index = MicroGameManager.Instance.GetSceneAccessIndex(SceneManager.GetSceneByName("Pilot"));
    int AccessCounts = MicroGameManager.Instance.SceneAccessCount[index]
    */

    public int GetSceneAccessIndex(Scene scene)
    {

        for(int i = 0; i < ScenePaths.Length; i++)
        {
            if (scene.path != "Assets/" + ScenePaths[i])
            {
                continue;
            }
            return i;
        }

        return -1;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class MicroGameManager : MonoBehaviour
{
    #region [ Instance ]
    public static MicroGameManager Instance => _instance ? _instance : _instance = FindObjectOfType<MicroGameManager>();
    private static MicroGameManager _instance;
    #endregion
    
    #region [ Scenes ]
    [SerializeField] private string _gameOverScene;
    [SerializeField] private string _testScene;
    [SerializeField] private SceneAsset sceneObject;
    #endregion
    
    #region [ Actions ]
    public Action Success;
    public Action Failure;
    #endregion

    #region [ Properties ]
    private string _activeScene;
    #endregion
    
    
    
    private void Awake()
    {
        Success += OnMicroGameSuccess;
        Failure += OnMicroGameFailure;
    }

    public void Start()
    {
        StartCoroutine(TransitionCoroutine(_testScene));
    }

    private IEnumerator TransitionCoroutine(string targetScene)
    {
        Scene scene = SceneManager.GetSceneByName(_activeScene);
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        loadOperation.allowSceneActivation = false;
        
        _activeScene = targetScene;

        yield return new WaitUntil(() => Mathf.Approximately(loadOperation.progress, .9f));
        if (scene.IsValid()) SceneManager.UnloadSceneAsync(scene);
        
        loadOperation.allowSceneActivation = true;
    }
    
    private void OnMicroGameSuccess()
    {
        StartCoroutine(TransitionCoroutine(_testScene));
    }

    private void OnMicroGameFailure()
    {
        StartCoroutine(TransitionCoroutine(_gameOverScene));
    }
}

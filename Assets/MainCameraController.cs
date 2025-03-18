using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    AudioListener audioListener;
    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        audioListener = GetComponent<AudioListener>();

        SceneManager.sceneLoaded += MainCameraController_SceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {

        audioListener.enabled = true;
        cam.enabled = true;
    }

    private void MainCameraController_SceneLoaded(Scene scene, LoadSceneMode LoadMode)
    {
        if (LoadMode == LoadSceneMode.Additive)
        {
            audioListener.enabled = false;
            cam.enabled = false;
        }
    }


}

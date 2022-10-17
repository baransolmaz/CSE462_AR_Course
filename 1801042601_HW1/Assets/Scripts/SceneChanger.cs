using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Camera cam;
    public void ChangeScene(string sceneName)
    {
        cam.enabled=false;
        SceneManager.LoadScene(sceneName);

    }
    public void Exit()
    {
        Application.Quit();
    }
}
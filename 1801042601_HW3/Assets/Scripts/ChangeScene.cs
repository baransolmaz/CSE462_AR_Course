using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Camera cam;
    public void changeScene(string sceneName)
    {
        cam.enabled=false;
        SceneManager.LoadScene(sceneName);

    }
    public void Exit()
    {
        Application.Quit();
    }
}

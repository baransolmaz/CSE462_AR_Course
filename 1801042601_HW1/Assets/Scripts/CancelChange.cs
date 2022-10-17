using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelChange : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public Button changeBut;
    public Button cancelBut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToBot()
    {
            cam1.enabled = false;
            cam2.enabled = true;
            cancelBut.interactable = false;
            changeBut.interactable = false;

      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    [SerializeField] private Button[] buttons;
    private int buttonNum;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var but in buttons) {
            but.interactable=false;
        }

    }

    // Update is called once per frame
    void Update()
    {

       
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            cam1.enabled = false;
            cam2.enabled = true;
            foreach(var but in buttons) {
                but.interactable=true;
            }
            
        }
    }
}

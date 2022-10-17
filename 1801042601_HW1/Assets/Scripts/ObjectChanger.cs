using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChanger : MonoBehaviour
{
    [SerializeField] public GameObject[] objects;
    public int selected=0;
    public Camera current;
    public Transform newLoc;
    public GameObject chooseCanvas;
    public GameObject moveCanvas;
    // Start is called before the first frame update
    void Start()
    {
        moveCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PrevCharacter () {
        objects[selected].SetActive(false);
        selected--;
        if(selected<0) {
            selected+=objects.Length;
        }
        objects[selected].SetActive(true);
    }
    public void NextCharacter () {
        objects[selected].SetActive(false);
        selected= (selected+1)% objects.Length;
        objects[selected].SetActive(true);
    }
    public void StartCharacter () {
        PlayerPrefs.SetInt("selected",selected);
        current.transform.position=newLoc.position;
        current.transform.rotation=newLoc.rotation;
        chooseCanvas.SetActive(false);
        moveCanvas.SetActive(true);
    }
}

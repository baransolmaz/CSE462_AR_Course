using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class FileOperation : MonoBehaviour
{
    public TextAsset file;
    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
        ReadAndClone(file,obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadAndClone(TextAsset ta,GameObject go){
        string[] fileLines1 = Regex.Split (ta.text, "\r\n");
        for ( int i=1; i <= Int64.Parse(fileLines1[0]); i+=1 ) {
            string line = fileLines1[i];
            string[] values = Regex.Split (line," " ); 
            GameObject clone =Instantiate(go, new Vector3(float.Parse(values[0]),float.Parse(values[1]),float.Parse(values[2])), Quaternion.identity);
            if (clone.GetComponent<LineRenderer> ()!=null)
            {
                LineRenderer LR=clone.GetComponent<LineRenderer> ();
                LR.SetPosition(0, clone.transform.position);
                LR.SetPosition(1, clone.transform.position);
            }
            clone.transform.parent=this.transform;
        }
    }
    public void Reset(){
        foreach (Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }
        ReadAndClone(file,obj);
    }
}

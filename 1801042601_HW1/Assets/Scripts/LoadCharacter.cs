using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] public GameObject[] objects;
    public Transform spawnPoint;
    void Start()
    {
        int selected=PlayerPrefs.GetInt("selected");
        GameObject prefab=objects[selected];
        //GameObject clone= Instantiate(prefab,spawnPoint.position,UnityEngine.Quaternion.identity);
        //clone.transform.position=spawnPoint.position;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolorGet : MonoBehaviour
{
    public GameObject obj;
    public GameObject parent;
    Renderer objRenderer;
    Renderer parentRenderer;
    // Start is called before the first frame update
    void Start()
    {
        objRenderer= obj.GetComponent<Renderer>();
        parentRenderer= parent.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        objRenderer.material.color=parentRenderer.material.color;
        obj.transform.position=parent.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject obj;
    public Renderer objRenderer;
    [SerializeField] private Color[] colors;
    private int colorValue;
    private Color newColor;
    // Start is called before the first frame update
    void Start()
    {
        objRenderer= obj.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor() {
        colorValue++;
        if (colorValue>3)
        {
            colorValue=0;
        }
        objRenderer.material.color=colors[colorValue];
    }
}

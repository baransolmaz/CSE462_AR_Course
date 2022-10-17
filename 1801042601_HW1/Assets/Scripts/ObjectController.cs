using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    Vector3 direction;

    private void Start() {

    }
    void Update()
    {
        direction =new Vector3(variableJoystick.Horizontal,0.0F,variableJoystick.Vertical);
        if (direction != Vector3.zero)
        {
            transform.position+= direction;
            transform.Translate(transform.position.normalized * speed *Time.deltaTime,Space.World);
        }
        
    }

}
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    private Rigidbody rb;
    public LayerMask layerMask;
    public bool ground;
    public Camera cam;
    Vector3 target;
    float speed=1.5f;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        SetNewTarget(new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z
        ));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)==true)
        {
            Ray ray=cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray.origin,ray.direction,out hitInfo)==true){

                SetNewTarget(new Vector3(hitInfo.point.x,transform.position.y,hitInfo.point.z));
            }
        }
        Vector3 dir= target-transform.position;
        if((int)target.x==(int)transform.position.x && (int)target.z== (int)transform.position.z){
            anim.SetBool("isWalking",false);
        }else
            anim.SetBool("isWalking",true);
        transform.Translate(dir.normalized * speed *Time.deltaTime,Space.World);
        
    }
    void SetNewTarget(Vector3 newTarget){
        target=newTarget;
        transform.LookAt(target);
    }
    
}

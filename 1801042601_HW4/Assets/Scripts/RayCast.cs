using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class RayCast : MonoBehaviour
{
    public Camera cam;
    public Light[] lights;
    // Adjustable parameters
    public int imageWidth = 640;
    public int imageHeight = 480;
    public float fov=90;

    public bool blackhole=false;
    public GameObject blackHoleObject;
    private Color back=new Color(0,Color.green.g/2,0);
    // Reference to the RawImage UI element
    public RawImage image;
    private Texture2D texture;
    private Ray[,] rays= new Ray[640,480];
    private bool[,] isHit= new bool[640,480];
    void Start(){
        texture = new Texture2D(640, 480);
        image.texture = texture;
        if (!blackhole){
            noBlackHole();
            createImage();
        }else
            for (int i=0;i<imageWidth; i++)
                for(int j = 0; j < imageHeight; j++){
                    rays[i,j]=cam.ViewportPointToRay(new Vector3((float)i/imageWidth, (float)j/imageHeight, 0));
                    isHit[i,j]=false;
                }
    }
    private void Update() {
        if (blackhole){
            if (blackHole()){
                createImage();
                blackhole=false;    
            }
        }
    }
    public bool blackHole () {
        bool isFinished=true;
        for (int x = 0; x < imageWidth; x++){
            for (int y = 0; y < imageHeight; y++){
                if (!isHit[x,y]){
                    RaycastHit hit;
                    if (Physics.Raycast(rays[x,y], out hit,1)){
                        isHit[x,y]=true;          
                        Color nC= getColor(hit);
                        texture.SetPixel(x, y,nC);
                    }else{
                        //if (y==imageHeight-1)
                        //    Debug.DrawRay(rays[x,y].origin, rays[x,y].direction, Color.green,20);
                        
                        isFinished=false;
                        rays[x,y].origin=rays[x,y].origin+(rays[x,y].direction.normalized);
                        Vector3 newDirection=(blackHoleObject.transform.position-rays[x,y].origin);
                        Vector3 dir=(newDirection.normalized)+ (25*rays[x,y].direction);
                        rays[x,y].direction=dir.normalized;
                    }
                }
            }
        }
        texture.Apply();
        return isFinished;
    }
    
    public void noBlackHole () {
        for (int i=0;i<imageWidth; i++){
            for(int j = 0; j < imageHeight; j++){
                Ray ray=cam.ViewportPointToRay(new Vector3((float)i/imageWidth, (float)j/imageHeight, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
                    Color nC=getColor(hit);
                    texture.SetPixel(i, j,nC);
                }else{
                    texture.SetPixel(i,j,back);
                }
                //if (y==imageHeight-1)
                    //Debug.DrawRay(ray.origin,1000*ray.direction, Color.green,20);
                
            }
        }
        texture.Apply();
   }
    Color getColor(RaycastHit hit){
        float hitCount=0;
        float totalIntensity=1;
        Color c= hit.collider.GetComponent<Renderer>().material.color;
        for (int k = 0; k < lights.Length; k++)
            if (RayFromLight(lights[k],hit.point)){
                totalIntensity+=lights[k].intensity;
                hitCount++;
            }
        hitCount/=lights.Length;
        totalIntensity/=lights.Length;
        return new Color((hitCount*totalIntensity*(c.r)),(hitCount*totalIntensity*(c.g)),(hitCount*totalIntensity*(c.b)));
    }
    bool RayFromLight(Light l,Vector3 hitPos){
        Vector3 dir = hitPos-l.transform.position;
        dir.Normalize();
        RaycastHit hit;
        Ray ray = new Ray(l.transform.position,dir);
        if (Physics.Raycast(ray , out hit, Mathf.Infinity))
            if (Vector3.Distance(hitPos,hit.point)<0.001)
                return true;
            //if(hit.point==hitPos) 
               // return true;
        return false;        
    }
    public void createImage(){
        if(!Directory.Exists(Application.dataPath+"/Images")) {
            Directory.CreateDirectory(Application.dataPath+"/Images");
        }
        AssetDatabase.Refresh();

        string filePath = Application.dataPath+"/Images/blackhole_"+blackhole+".png";
        byte[] bytes = texture.EncodeToPNG();
        FileStream fileStream = File.Create(filePath);
        fileStream.Write(bytes, 0, bytes.Length);

        fileStream.Close();        
        AssetDatabase.Refresh();
    }
}

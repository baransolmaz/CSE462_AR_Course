using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Accord.Math.Decompositions;
using Accord;

public class ChangeImage : MonoBehaviour
{
    int currn=0;
    public List<Sprite> images= new List<Sprite>();
    public Image image;
    public GameObject teapot;
    private List<double[,]> centerPoints=new List<double[,]>();
    private List<double[,]> rotationWay=new List<double[,]>();
    double[,] mainXY = new double[4,2] { {1400 ,1444  },{ 1492,1774   },{1548 ,1581  },{ 1908,1729 } };
    double[,] mainUV = new double[4,2] { {-9.16,-8.56 },{-5.15,-21.39 },{-3.16,-13.95},{10.93,-19.6 } };
    double[,] homo;
    float[] baseScale=new float[2]{1,1};
    private GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        centerPoints.Add(new double[3,1] { {1908},{1729},{1} });//5315
        centerPoints.Add(new double[3,1] { {2131},{1635},{1} });//5316
        centerPoints.Add(new double[3,1] { {2795},{1534},{1} });//5318
        centerPoints.Add(new double[3,1] { {2912},{1621},{1} });//5319
        centerPoints.Add(new double[3,1] { {2478},{1795},{1} });//5320
        centerPoints.Add(new double[3,1] { {2504},{1830},{1} });//5321
        centerPoints.Add(new double[3,1] { {2043},{1811},{1} });//5322
        centerPoints.Add(new double[3,1] { {1859},{1777},{1} });//5323
        centerPoints.Add(new double[3,1] { {1875},{1698},{1} });//5324
        centerPoints.Add(new double[3,1] { {1957},{1585},{1} });//5325
        centerPoints.Add(new double[3,1] { {2615},{2146},{1} });//5326
        centerPoints.Add(new double[3,1] { {2306},{1563},{1} });//5327
        centerPoints.Add(new double[3,1] { {2132},{1579},{1} });//5328
        centerPoints.Add(new double[3,1] { {1539},{1388},{1} });//5329
        centerPoints.Add(new double[3,1] { {1910},{1478},{1} });//5330
        centerPoints.Add(new double[3,1] { {2054},{1645},{1} });//5331
        centerPoints.Add(new double[3,1] { {2154},{1495},{1} });//5332
        centerPoints.Add(new double[3,1] { {2625},{1371},{1} });//5333
        centerPoints.Add(new double[3,1] { {2220},{1651},{1} });//5334
        
        rotationWay.Add(new double[3,1] { {1548},{1581},{1} });//5315
        rotationWay.Add(new double[3,1] { {1755},{1522},{1} });//5316
        rotationWay.Add(new double[3,1] { {2344},{1551},{1} });//5318
        rotationWay.Add(new double[3,1] { {2409},{1675},{1} });//5319
        rotationWay.Add(new double[3,1] { {1946},{1734},{1} });//5320
        rotationWay.Add(new double[3,1] { {1954},{1734},{1} });//5321
        rotationWay.Add(new double[3,1] { {1507},{1667},{1} });//5322
        rotationWay.Add(new double[3,1] { {1343},{1594},{1} });//5323
        rotationWay.Add(new double[3,1] { {1472},{1598},{1} });//5324
        rotationWay.Add(new double[3,1] { {1633},{1522},{1} });//5325
        rotationWay.Add(new double[3,1] { {1908},{2035},{1} });//5326
        rotationWay.Add(new double[3,1] { {1577},{1758},{1} });//5327
        rotationWay.Add(new double[3,1] { {1333},{2017},{1} });//5328
        rotationWay.Add(new double[3,1] { {950},{1203},{1} });//5329
        rotationWay.Add(new double[3,1] { {1267},{1251},{1} });//5330
        rotationWay.Add(new double[3,1] { {1456},{1497},{1} });//5331
        rotationWay.Add(new double[3,1] { {1506},{1613},{1} });//5332
        rotationWay.Add(new double[3,1] { {2054},{1445},{1} });//5333
        rotationWay.Add(new double[3,1] { {1574},{1682},{1} });//5334

        homo=calculateHomography(mainXY,mainUV);
        double[,] projection = calculateProjection(homo,centerPoints[0]);
        double[,] rotationPoint = calculateProjection(homo,rotationWay[0]);
        float[] s=getScale(centerPoints[0],rotationWay[0],projection,rotationPoint);
        baseScale=new float[2]{s[0],s[1]};
        float angle =getAngle(projection,rotationPoint);
        clone =Instantiate(teapot, new Vector3((float)(projection[0,0]),(float)(projection[1,0]),(float)(projection[2,0]-6)), Quaternion.identity);
        clone.transform.eulerAngles = new Vector3(30,180- angle, 0);
        Vector3 localScale=clone.transform.localScale;
        clone.transform.localScale = new Vector3((localScale.x*s[0])/baseScale[0],(localScale.y*s[1])/baseScale[0],(localScale.z*s[0])/baseScale[0]);
    }

    public void next () {
        currn=(currn+1)%19;
        image.sprite=images[currn];
        double[,] projection = calculateProjection(homo,centerPoints[currn]);
        double[,] rotationPoint = calculateProjection(homo,rotationWay[currn]);
        float[] s=getScale(centerPoints[currn],rotationWay[currn],projection,rotationPoint);
        float angle =getAngle(projection,rotationPoint);
        Destroy(clone);
        clone =Instantiate(teapot, new Vector3((float)(projection[0,0]),(float)(projection[1,0]),(float)(projection[2,0]-6)), Quaternion.identity);
        clone.transform.eulerAngles =  new Vector3(30,180- angle, 0);
        Vector3 localScale=clone.transform.localScale;
        clone.transform.localScale = new Vector3((localScale.x*s[0])/baseScale[0],(localScale.y*s[1])/baseScale[0],(localScale.z*s[0])/baseScale[0]);
    }
    public void prev () {
        currn=(currn+18)%19;
        image.sprite=images[currn];
        double[,] projection = calculateProjection(homo,centerPoints[currn]);
        double[,] rotationPoint = calculateProjection(homo,rotationWay[currn]);
        float[] s=getScale(centerPoints[currn],rotationWay[currn],projection,rotationPoint);
        float angle =getAngle(projection,rotationPoint);
        Destroy(clone);
        clone =Instantiate(teapot, new Vector3((float)(projection[0,0]),(float)(projection[1,0]),(float)(projection[2,0]-6)), Quaternion.identity);
        clone.transform.eulerAngles =  new Vector3(30,180- angle, 0);
        Vector3 localScale=clone.transform.localScale;
        clone.transform.localScale = new Vector3((localScale.x*s[0])/baseScale[0],(localScale.y*s[1])/baseScale[0],(localScale.z*s[0])/baseScale[0]);
    }
    private double[,] calculateHomography(double[,] p1, double[,] p2){
        double[,] A = new double[2 * p1.GetLength(0), 9];
        int p = 0;
        for (int i = 0; i < 2 * p1.GetLength(0); i++){
            if (i % 2 == 0){
                A[i, 0] = -1 * p1[p, 0];
                A[i, 1] = -1 * p1[p, 1];
                A[i, 2] = -1;
                A[i, 3] = 0;
                A[i, 4] = 0;
                A[i, 5] = 0;
                A[i, 6] = p1[p, 0] * p2[p, 0];
                A[i, 7] = p2[p, 0] * p1[p, 1];
                A[i, 8] = p2[p, 0];
            }else{
                A[i, 0] = 0;
                A[i, 1] = 0;
                A[i, 2] = 0;
                A[i, 3] = -1 * p1[p, 0];
                A[i, 4] = -1 * p1[p, 1];
                A[i, 5] = -1;
                A[i, 6] = p1[p, 0] * p2[p, 1];
                A[i, 7] = p2[p, 1] * p1[p, 1];
                A[i, 8] = p2[p, 1];
                ++p;
            }
        }
        SingularValueDecomposition svd=new SingularValueDecomposition(A,true,true);
        double[,] V=svd.RightSingularVectors;
        double[,] VT = transpose(V);
        double[] x =getRow(VT ,VT.GetLength(0)-1);

        double[,] homo = new double[3, 3];

        int row = 0;
        for (int i = 0; i < x.Length; i++){
            if (i % 3 == 0 && i != 0)
                ++row;
            homo[row, i % 3] = x[i];
        }
        return homo;
    }
    private double[,] transpose(double[,] mat){
        return Accord.Math.Matrix.Transpose(mat);
    }
    private double[,] product(double[,] mat1,double[,] mat2){
        return Accord.Math.Matrix.Dot(mat1,mat2);
    }
    private double[,] inverse(double[,] mat){
        return Accord.Math.Matrix.Inverse(mat);
    }
    private double[] getRow(double[,] matrix, int rowNumber){
        double[] row= new double[matrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(1); i++)
            row[i]=matrix[rowNumber,i];   
        return row;
    }
    private double[,] calculateProjection(double[,] homo,double[,] scenePoint){
        double[,] prj = product(homo, scenePoint);
        prj[0,0]=prj[0,0]/prj[2,0];
        prj[1,0]=prj[1,0]/prj[2,0];
        prj[2,0]=prj[2,0]/prj[2,0];
        return prj;
    }
    private double getDistance(double[,] p1,double[,] p2){
        return  Math.Sqrt(Math.Pow(p1[0,0]-p2[0,0],2)+Math.Pow(p1[1,0]-p2[1,0],2));
    }
    private float[] getScale(double[,] c,double[,] r,double[,] c1,double[,] r1 ){
        double d1=getDistance(c,r);
        double d2=getDistance(c1,r1);
        double s=(d2/d1);
        return  new float[2] { (float)s,(float)s};
    }
    private float getAngle(double[,] projection,double[,] rotationPoint) {
        Vector2 center =new Vector2((float)projection[0,0],(float)projection[1,0]);
        Vector2 teapotWay =new Vector2((float)(projection[0,0]+5),(float)projection[1,0]);
        Vector2 rotWay =new Vector2((float)rotationPoint[0,0],(float)rotationPoint[1,0]);
        return Vector2.Angle(center-teapotWay,rotWay-center);
    }
}

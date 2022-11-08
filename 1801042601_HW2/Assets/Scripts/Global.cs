using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Accord.Math.Decompositions;
using Accord;

public class Global : MonoBehaviour
{
    public GameObject Reds;
    public GameObject Blues;
    private double ScaleX=1.0,ScaleY=1.0,ScaleZ=1.0;
    private int move=0;
    private int line_draw=1;
    private double[,] RRot;

    public void doAlig() {
        double[,] list_red=GetAllChilds(Reds);
        double[,] list_blue=GetAllChilds(Blues);
        for (int i = 0; i < list_red.GetLength(0); i++){
            for (int j = 0; j < list_red.GetLength(0); j++){
                if (i==j)
                    j++;
                for (int k = 0; k < list_red.GetLength(0)&& (j<list_red.GetLength(0)); k++){
                    if ((i==k || k==j))
                        k++;
                    if ((i==k || k==j))
                        k++;
                    if (!(k<list_red.GetLength(0)))
                        break;
                    List<Vector3> red_group= new List<Vector3>(){
                        new Vector3((float)list_red[i,0],(float)list_red[i,1],(float)list_red[i,2]),
                        new Vector3((float)list_red[j,0],(float)list_red[j,1],(float)list_red[j,2]),
                        new Vector3((float)list_red[k,0],(float)list_red[k,1],(float)list_red[k,2])};
                                
                    for (int bi = 0; bi < list_blue.GetLength(0); bi++){
                        for (int bj =0; bj < list_blue.GetLength(0); bj++){
                            if (bi==bj)
                                bj++;
                            for (int bk = 0; (bk < list_blue.GetLength(0)) && (bj<list_blue.GetLength(0)); bk++){
                                if ((bi==bk || bk==bj))
                                    bk++;
                                if ((bi==bk || bk==bj))
                                    bk++;
                                if (!(bk<list_blue.GetLength(0)))
                                    break;
                                float x_r=0,y_r=0,z_r=0;
                                float x_b=0,y_b=0,z_b=0;
                                List<Vector3> blue_group= new List<Vector3>(){
                                    new Vector3((float)list_blue[bi,0],(float)list_blue[bi,1],(float)list_blue[bi,2]),
                                    new Vector3((float)list_blue[bj,0],(float)list_blue[bj,1],(float)list_blue[bj,2]),
                                    new Vector3((float)list_blue[bk,0],(float)list_blue[bk,1],(float)list_blue[bk,2])};
                                for (int l = 0; l <3; l++){
                                    x_r+=red_group[l].x/3;
                                    y_r+=red_group[l].y/3;
                                    z_r+=red_group[l].z/3;
                                    x_b+=blue_group[l].x/3;
                                    y_b+=blue_group[l].y/3;
                                    z_b+=blue_group[l].z/3;
                                }
                                Vector3 vec_red= new Vector3(x_r,y_r,z_r);
                                Vector3 vec_blue= new Vector3(x_b,y_b,z_b);
                                for (int l = 0; l <3; l++){
                                    red_group[l]-= vec_red;
                                    blue_group[l]-=vec_blue;
                                }
                                if (!checkDistance(red_group,blue_group))
                                    break;
                                for (int ii = 0; ii < 3; ii++){
                                    red_group[ii]=new Vector3((float)(red_group[ii].x*ScaleX),(float)(red_group[ii].y*ScaleY),(float)(red_group[ii].z*ScaleZ));
                                }
                                double[,] H=product(transpose(convertToArray(red_group)),convertToArray(blue_group));

                                SingularValueDecomposition svd=new SingularValueDecomposition(H,true,true);
                                double[,] V=svd.RightSingularVectors;
                                double[,] U=svd.LeftSingularVectors;
                                double[,] R= product(V,transpose(U));

                                if(determinant(R)< 0.0) {
                                    SingularValueDecomposition svd1=new SingularValueDecomposition(R,true,true);
                                    V=svd1.RightSingularVectors;
                                    U=svd1.LeftSingularVectors;
                                    for (int kk = 0; kk < 3; kk++)
                                        V[2,kk]*=-1;
                                    R= product(V,transpose(U)); 
                                }
                                double[,] T=translation(vec_blue,R,vec_red);
                                if (checkMatrix(list_blue,R,list_red,T)){
                                    print("G-R: ");
                                    for(int ii = 0; ii <3; ii++) {
                                        print("G "+R[ii,0]+" "+R[ii,1]+" "+R[ii,2]);
                                    }    
                                    print("G-T: ");
                                    print("G "+T[0,0]+" "+T[1,0]+" "+T[2,0]);
                                    print("G-S: ");
                                    print("G "+ScaleX+" "+ScaleY+" "+ScaleZ);
                                    move=1;
                                    return;
                                }
                            }
                        }
                    }            
                }
            }
        }
        
    }
    public double[,] GetAllChilds(GameObject Go){
        double[,]arr=new double[ Go.transform.childCount,3];
        for(int i = 0; i < Go.transform.childCount; i++) {
            arr[i,0]=Go.transform.GetChild(i).gameObject.transform.position.x;
            arr[i,1]=Go.transform.GetChild(i).gameObject.transform.position.y;
            arr[i,2]=Go.transform.GetChild(i).gameObject.transform.position.z;
        }
        return arr;
    }
    private double[,] transpose(double[,] mat){
        return Accord.Math.Matrix.Transpose(mat);
    }
    private double[,] product(double[,] mat1,double[,] mat2){
        return Accord.Math.Matrix.Dot(mat1,mat2);
    }
    private double[,] convertToArray(List<Vector3> mat){
        double[,] m = new double[3,3];
        for (int i = 0; i <3; i++){
            m[i,0]=mat[i].x;
            m[i,1]=mat[i].y;
            m[i,2]=mat[i].z;
        }
        return m;
    }
    private double determinant(double[,] mat){
        double det=0.0;
        for(int i=0;i<3;i++)
            det +=(mat[0,i]*(mat[1,(i+1)%3]*mat[2,(i+2)%3] - mat[1,(i+2)%3]*mat[2,(i+1)%3]));
        return det;
    }
    private bool checkMatrix(double[,] blues,double[,] R,double[,] reds,double[,] T){
        double[,] SS=new double[3,3]{{ ScaleX,0,0}, {0,ScaleY,0},{0,0,ScaleZ }};
        double[,] roteded=product(R,product(SS,transpose(reds)));
        RRot=transpose(roteded);
        int count=0;
        int RRot_length=RRot.GetLength(0);
        int blue_length=blues.GetLength(0);
        for (int i = 0; i <RRot_length; i++){
            RRot[i,0]+=T[0,0];
            RRot[i,1]+=T[1,0];
            RRot[i,2]+=T[2,0];
        }
        for (int i = 0; i <RRot_length; i++){
            for (int j = 0; j <blue_length; j++){
                
                if(compare(getRow(RRot,i),getRow(blues,j)))
                    count++;
                if ((i>(RRot_length/2)) && (count<=0)){
                    return false;}
                if(count>=(3*(RRot_length/5))){
                    return true;
                }
            }
        }
        if(count>=(RRot_length/2)){
            return true;
        }else
            return false;
    }
    private double[,] translation(Vector3 vec_blue, double[,] R, Vector3 vec_red){
        double[,] centerA= new double[3,1];
        double[,] centerB= new double[3,1];
        centerA[0,0]=vec_red.x*ScaleX;
        centerA[1,0]=vec_red.y*ScaleY;
        centerA[2,0]=vec_red.z*ScaleZ;
        centerB[0,0]=vec_blue.x;
        centerB[1,0]=vec_blue.y;
        centerB[2,0]=vec_blue.z;
        double[,] rotatedA=product(R,centerA);
        for (int i = 0; i <3; i++)
            centerB[i,0]-=rotatedA[i,0];
        return centerB;
    }
    private bool compare(double[] L,double[] R){
        double[] left= new double[3];
        double[] right=new double[3];
        for (int i = 0; i < 3; i++){
            left[i]=Math.Round(L[i],3);
            right[i]=Math.Round(R[i],3);
        }
        if((left[0]==right[0]) &&(left[1]==right[1])&& (left[2]==right[2]))
            return true;
        return false;
    }
    private double[] getRow(double[,] arr,int index){
        double[] row=new double[3];
        for (int i = 0; i < 3; i++)
            row[i]=arr[index,i];
        return row;
    }
    private void Update() {
        if (move==1){
            int RRot_length=RRot.GetLength(0);
            for (int k = 0; k < RRot_length; k++){
                Vector3 target=new Vector3((float)RRot[k,0],(float)RRot[k,1],(float)RRot[k,2]);
                if (Reds.transform.GetChild(k).gameObject.transform.position != target){
                    Reds.transform.GetChild(k).gameObject.transform.position = Vector3.MoveTowards(Reds.transform.GetChild(k).gameObject.transform.position,target, 0.5f * Time.deltaTime);
                    if (line_draw==1){
                        LineRenderer LR = Reds.transform.GetChild(k).gameObject.GetComponent<LineRenderer> ();
                        LR.SetPosition(1,target);
                    }
                }
            }
            line_draw=0;
        }
    }
    public void Reset() {
        move=0;
        line_draw=1;
        ScaleX=1.0;
        ScaleY=1.0;
        ScaleZ=1.0;
    }
    private bool checkDistance(List<Vector3> red_group,List<Vector3> blue_group){
        float[] DisA = new float[3];
        float[] DisB = new float[3];
        for (int i = 0; i < 3; i++){
            //for (int j = 0; j < 3; j++)
            {
                /* DisA[i,j] = Math.Abs(red_group[i,j]-red_group[(i+1)%3,j]);
                DisB[i,j] = Math.Abs(blue_group[i,j]-blue_group[(i+1)%3,j]); */
                DisA[i]=Vector3.Distance(red_group[i],red_group[(i+1)%3]);
                DisB[i]=Vector3.Distance(blue_group[i],blue_group[(i+1)%3]);
            }
        }
        if (scale(DisA,DisB)){
            ScaleX=Math.Round(DisB[0]/DisA[0],3);
            ScaleX=Math.Round(DisB[1]/DisA[1],3);
            ScaleX=Math.Round(DisB[2]/DisA[2],3);
            return true;
        }
        return false;
    }
    private bool scale(float[] L,float[] R){
        //int count=0;
        //for (int i = 0; i < 3; i++)
        if ((Math.Round(R[0]/L[0],3)==Math.Round(R[1]/L[1],3)) && (Math.Round(R[1]/L[1],3)==Math.Round(R[2]/L[2],3)))
            return true;
        return false;
    }
}

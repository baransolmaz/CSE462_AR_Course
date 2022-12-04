using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Accord.Math.Decompositions;
using Accord;

public class Part1 : MonoBehaviour
{
    public double[,] points1 = new double[4, 2] { { 11, 12 }, { 56, 43 }, { 21, 19 }, { 31, 16 } };
    public double[,] points2 = new double[4, 2] { { 11, 12 }, { 56, 43 }, { 21, 19 }, { 31, 16 } };
    
    public double[,] points3 = new double[4, 2] { { 2, 5 }, {6, 7 }, { 3, 8 }, { 2, 9 } };

    public void part1_1() {
        Debug.Log("1.1");
        double[,] homo=calculateHomography(points1,points2);

        for (int i = 0; i < homo.GetLength(0); i++){
            Debug.Log(i+": "+homo[i,0]+" "+homo[i,1]+" "+homo[i,2]);
        }
    }
    public void part1_2() {
        Debug.Log("1.2");
        Debug.Log("Not Implemented");
    }
    public void part1_3() {
        double x=3, y=8;
        Debug.Log("1.3");
        double[,] homo=calculateHomography(points1,points2);
        double[,] point=new double[3,1]{{x},{y},{1}};
        double[,] projection = calculateProjection(homo,point);

        Debug.Log("Projection of scene point: "+projection[0,0]+" "+projection[1,0]+" "+projection[2,0]);
    }
    public void part1_4() {
        double x=3, y=8;
        Debug.Log("1.4");
        double[,] homo=calculateHomography(points1,points2);
        double[,] point=new double[3,1]{{x},{y},{1}};
        double[,] projection = calculateProjectionV2(homo,point);

        Debug.Log("Projection of image point: "+projection[0,0]+" "+projection[1,0]+" "+projection[2,0]);
    }
    public void part1_5() {
        Debug.Log("1.5");

        double[,] mainP = new double[5, 2] { { 900,700 }, {0,700 }, {0,0 }, {900,0 }, { 400, 300 } };
        double[,] image1 = new double[5, 2] { { 472,515 }, {448,2616 }, {2072,2621 }, {2091, 534 } , {1385,1687 } };
        double[,] image2 = new double[5, 2] { { 283,507 }, {249,2609 }, { 1924,2695 }, {1948,446 }, {1192,1682 } };
        double[,] image3 = new double[5, 2] { { 491,595 }, {486,2681 }, {1945,2601 }, {2148,719 }, {1399,1827 } };
        
        double[,] homo1=calculateHomography(mainP,image1);
        double[,] homo2=calculateHomography(mainP,image2);
        double[,] homo3=calculateHomography(mainP,image3);

        List<double[,]> mainP2 = new List<double[,]>();  
        List<double[,]> relatedP = new List<double[,]>();
 
        mainP2.Add(new double[3, 1] { { 100 }, { 100 }, { 1 } });
        mainP2.Add(new double[3, 1] { { 300 }, { 400 }, { 1 } });
        mainP2.Add(new double[3, 1] { { 600 }, { 700 }, { 1 } });
 
        Debug.Log("Projecting Image 1");
        relatedP.Add(new double[3, 1] { { 1837 }, { 2383 }, { 1 } });
        relatedP.Add(new double[3, 1] { { 1153 }, { 1915 }, { 1 } });
        relatedP.Add(new double[3, 1] { { 468 }, { 1218 }, { 1 } });

        calculateMatchAndErr(homo1, mainP2, relatedP);

        Debug.Log("Projecting Image 2");
        relatedP = new List<double[,]>();

        relatedP.Add(new double[3, 1] { { 1670 }, { 2426 }, { 1 } });
        relatedP.Add(new double[3, 1] { { 953 }, { 1917 }, { 1 } });
        relatedP.Add(new double[3, 1] { { 276 }, { 1207 }, { 1 } });

        calculateMatchAndErr(homo2, mainP2, relatedP);

        Debug.Log("Projecting Image 3");
        relatedP = new List<double[,]>();

        relatedP.Add(new double[3, 1] { { 1763 }, {2430 }, { 1 } });
        relatedP.Add(new double[3, 1] { { 1172 }, { 2039 }, { 1 } });
        relatedP.Add(new double[3, 1] { { 494 }, { 1359 }, { 1 } });

        calculateMatchAndErr(homo3, mainP2, relatedP);
        
    }
    public void part1_6() {
        Debug.Log("1.6");
        double[,] mainP = new double[5, 2] { { 900,700 }, {0,700 }, {0,0 }, {900,0 }, { 400, 300 } };
        double[,] image1 = new double[5, 2] { { 472,515 }, {448,2616 }, {2072,2621 }, {2091, 534 } , {1385,1687 } };
        double[,] image2 = new double[5, 2] { { 283,507 }, {249,2609 }, { 1924,2695 }, {1948,446 }, {1192,1682 } };
        double[,] image3 = new double[5, 2] { { 491,595 }, {486,2681 }, {1945,2601 }, {2148,719 }, {1399,1827 } };
        
        double[,] homo1=calculateHomography(mainP,image1);
        double[,] homo2=calculateHomography(mainP,image2);
        double[,] homo3=calculateHomography(mainP,image3);

        double[,] s1 = new double[3, 1] { { 75 }, {55 }, {1 } };
        double[,] s2 = new double[3, 1] { { 63 }, {33 }, {1 } };
        double[,] s3 = new double[3, 1] { { 1 }, {1}, {1} };

        Debug.Log("Projection of s1: ");
        double[,] projection1 = calculateProjection(homo1,s1);
        double[,] projection2 = calculateProjection(homo2,s1);
        double[,] projection3 = calculateProjection(homo3,s1);
        Debug.Log(projection1[0,0]+" "+projection1[1,0]+" "+projection1[2,0]);
        Debug.Log(projection2[0,0]+" "+projection2[1,0]+" "+projection2[2,0]);
        Debug.Log(projection3[0,0]+" "+projection3[1,0]+" "+projection3[2,0]);

        Debug.Log("Projection of s2: ");
        projection1 = calculateProjection(homo1,s2);
        projection2 = calculateProjection(homo2,s2);
        projection3 = calculateProjection(homo3,s2);
        Debug.Log(projection1[0,0]+" "+projection1[1,0]+" "+projection1[2,0]);
        Debug.Log(projection2[0,0]+" "+projection2[1,0]+" "+projection2[2,0]);
        Debug.Log(projection3[0,0]+" "+projection3[1,0]+" "+projection3[2,0]);

        Debug.Log("Projection of s3: ");
        projection1 = calculateProjection(homo1,s3);
        projection2 = calculateProjection(homo2,s3);
        projection3 = calculateProjection(homo3,s3);
        Debug.Log(projection1[0,0]+" "+projection1[1,0]+" "+projection1[2,0]);
        Debug.Log(projection2[0,0]+" "+projection2[1,0]+" "+projection2[2,0]);
        Debug.Log(projection3[0,0]+" "+projection3[1,0]+" "+projection3[2,0]);
        
    }
    public void part1_7() {
        Debug.Log("1.7");
        double[,] mainP = new double[5, 2] { { 900,700 }, {0,700 }, {0,0 }, {900,0 }, { 400, 300 } };
        double[,] image1 = new double[5, 2] { { 472,515 }, {448,2616 }, {2072,2621 }, {2091, 534 } , {1385,1687 } };
        double[,] image2 = new double[5, 2] { { 283,507 }, {249,2609 }, { 1924,2695 }, {1948,446 }, {1192,1682 } };
        double[,] image3 = new double[5, 2] { { 491,595 }, {486,2681 }, {1945,2601 }, {2148,719 }, {1399,1827 } };
        
        double[,] homo1=calculateHomography(mainP,image1);
        double[,] homo2=calculateHomography(mainP,image2);
        double[,] homo3=calculateHomography(mainP,image3);

        double[,] i1 = new double[3, 1] { { 500 }, {400 }, {1 } };
        double[,] i2 = new double[3, 1] { { 86 }, {167 }, {1 } };
        double[,] i3 = new double[3, 1] { { 10 }, {10}, {1} };

        Debug.Log("Projection of i1: ");
        double[,] projection1 = calculateProjectionV2(homo1,i1);
        double[,] projection2 = calculateProjectionV2(homo2,i1);
        double[,] projection3 = calculateProjectionV2(homo3,i1);
        Debug.Log(projection1[0,0]+" "+projection1[1,0]+" "+projection1[2,0]);
        Debug.Log(projection2[0,0]+" "+projection2[1,0]+" "+projection2[2,0]);
        Debug.Log(projection3[0,0]+" "+projection3[1,0]+" "+projection3[2,0]);

        Debug.Log("Projection of i2: ");
        projection1 = calculateProjectionV2(homo1,i2);
        projection2 = calculateProjectionV2(homo2,i2);
        projection3 = calculateProjectionV2(homo3,i2);
        Debug.Log(projection1[0,0]+" "+projection1[1,0]+" "+projection1[2,0]);
        Debug.Log(projection2[0,0]+" "+projection2[1,0]+" "+projection2[2,0]);
        Debug.Log(projection3[0,0]+" "+projection3[1,0]+" "+projection3[2,0]);

        Debug.Log("Projection of i3: ");
        projection1 = calculateProjectionV2(homo1,i3);
        projection2 = calculateProjectionV2(homo2,i3);
        projection3 = calculateProjectionV2(homo3,i3);
        Debug.Log(projection1[0,0]+" "+projection1[1,0]+" "+projection1[2,0]);
        Debug.Log(projection2[0,0]+" "+projection2[1,0]+" "+projection2[2,0]);
        Debug.Log(projection3[0,0]+" "+projection3[1,0]+" "+projection3[2,0]);
        
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
    private double[,] calculateProjectionV2(double[,] homo,double[,] imagePoint){
        double[,] prj = product(inverse(homo), imagePoint);
        prj[0,0]=prj[0,0]/prj[2,0];
        prj[1,0]=prj[1,0]/prj[2,0];
        prj[2,0]=prj[2,0]/prj[2,0];
        return prj;
    }
    private void calculateMatchAndErr(double[,] homo, List<double[,]> mainP2, List<double[,]> related){

        Debug.Log("Calculated Homography Matrix : \n");
        for (int i = 0; i < homo.GetLength(0); i++){
            string log = "";
            for (int j = 0; j < homo.GetLength(1); j++)
                log += homo[i, j] + " ";
            Debug.Log(log);
        }

        Debug.Log("Calculating Projection of points...");
        int k = 0;
        foreach (double[,] xy in mainP2){
            double[,] uv = related[k++];
            var res = calculateProjection(homo, xy);  
            Debug.Log("Error : %" + calculateError(res, uv));
        }
    }
    private double calculateError(double[,] result, double[,] actual){
        double x1 = result[0, 0];
        double x2 = actual[0, 0];

        double y1 = result[1, 0];
        double y2 = actual[1, 0];

        double resDif = Math.Round(Math.Sqrt((x1*x1) +(y1*y1)),3);
        double actualDif =Math.Round(Math.Sqrt((x2*x2) +(y2*y2)),3);

        return Math.Abs((actualDif - resDif) / actualDif * 100);
    }
}

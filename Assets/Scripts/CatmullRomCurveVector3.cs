using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatmullRomCurveVector3
{
    CatmullRomCurve curveX;
    CatmullRomCurve curveY;
    CatmullRomCurve curveZ;


    public CatmullRomCurveVector3(Vector3[] points){
        curveX = new CatmullRomCurve(points.Select(point => point.x).ToArray());
        curveY = new CatmullRomCurve(points.Select(point => point.y).ToArray());
        curveZ = new CatmullRomCurve(points.Select(point => point.z).ToArray());
    }

    public Vector3 CalcPos(float t){
        return new Vector3(
            curveX.Calc(t).position,
            curveY.Calc(t).position,
            curveZ.Calc(t).position
        );
    }

    public Vector3 CalcVel(float t){
        return new Vector3(
            curveX.Calc(t).velocity,
            curveY.Calc(t).velocity,
            curveZ.Calc(t).velocity
        );
    }

    public Vector3 CalcSecondDerivative(float t){
        return new Vector3(
            curveX.CalcSecondDerivative(t),
            curveY.CalcSecondDerivative(t),
            curveZ.CalcSecondDerivative(t)
        );
    }
    
}

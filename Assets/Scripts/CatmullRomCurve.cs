using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomCurve
{
    readonly IReadOnlyList<Spline> splines;

    public CatmullRomCurve(float[] points){
        Spline[] splines = new Spline[points.Length - 1];
        for(int i = 0; i < points.Length - 1; i++){
            splines[i] = new Spline(
                points[i],
                points[i+1],
                i == 0 ? points[i+1] - points[i] : (points[i+1] - points[i-1]) / 2,
                i == points.Length - 2 ? points[i] - points[i-1] : (points[i+2] - points[i]) / 2
            );
        }
        this.splines = splines;
    }

    public PositionAndVelocity<float, float> Calc(float t){
        int index = t < 1 ? Mathf.FloorToInt(t * splines.Count) : splines.Count - 1;
        float t_in_spline = t < 1 ? t * splines.Count % 1 : 1;
        return splines[index].Calc(t_in_spline);
    }

    public float CalcSecondDerivative(float t){
        int index = t < 1 ? Mathf.FloorToInt(t * splines.Count) : splines.Count - 1;
        float t_in_spline = t < 1 ? t * splines.Count % 1 : 1;
        return splines[index].CalcSecondDerivative(t_in_spline);
    }
}

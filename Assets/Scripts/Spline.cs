using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline
{
    readonly float a, b, c, d;
    public PositionAndVelocity<float, float> Calc(float t){
        return new PositionAndVelocity<float, float>(
            a * t*t*t +     b * t*t +     c * t + d,
                        3 * a * t*t + 2 * b * t + c
        );
    }

    public Spline(float p0, float p1, float v0, float v1){
        a =  2 * p0 - 2 * p1 +     v0 + v1;
        b = -3 * p0 + 3 * p1 - 2 * v0 - v1;
        c = v0;
        d = p0;
    }
}

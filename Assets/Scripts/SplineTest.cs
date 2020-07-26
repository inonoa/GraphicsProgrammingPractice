using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class SplineTest : MonoBehaviour
{
    [SerializeField] Vector2[] points;

    void Start()
    {

    }

    [Button]
    void Move(){

        var curveX = new CatmullRomCurve(points.Select(vec => vec.x).ToArray());
        var curveY = new CatmullRomCurve(points.Select(vec => vec.y).ToArray());

        StartCoroutine(Move(curveX, curveY));
    }

    IEnumerator Move(CatmullRomCurve curveX, CatmullRomCurve curveY){
        float time = 0;
        transform.position = new Vector3(curveX.Calc(0).position, curveY.Calc(0).position, 0);
        transform.rotation = Quaternion.LookRotation(
            new Vector3(curveX.Calc(0).velocity, curveY.Calc(0).velocity, 0)
        );
        yield return null;
        while((time += Time.deltaTime) <= 10){
            transform.position = new Vector3(curveX.Calc(time / 10).position, curveY.Calc(time / 10).position, 0);
            transform.rotation = Quaternion.LookRotation(
                new Vector3(curveX.Calc(time / 10).velocity, curveY.Calc(time / 10).velocity, 0)
            );
            yield return null;
        }
    }


    void Update()
    {
        
    }
}

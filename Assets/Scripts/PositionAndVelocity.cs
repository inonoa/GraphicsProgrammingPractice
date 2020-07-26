using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAndVelocity<P, V>
{
    public PositionAndVelocity(P position, V velocity){
        this.position = position;
        this.velocity = velocity;
    }

    public readonly P position;
    public readonly V velocity;
    
}

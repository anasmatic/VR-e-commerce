using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Location{
    public float lat;
    public float lng;

    public override string ToString()
    {
        return base.ToString() + " : "+lat+" ^ "+lng;
    }
}
[Serializable]
public class Geometry{
    public Location location;
}

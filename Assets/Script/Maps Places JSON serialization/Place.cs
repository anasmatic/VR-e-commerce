using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Place  {
    public Geometry geometry;
    public string icon;
    public string id;
    public string name;

    public override string ToString()
    {
        return base.ToString() + ": name:" + name + "\n icon:" + icon+ "\n geometry"+geometry.location.ToString();
    }
}

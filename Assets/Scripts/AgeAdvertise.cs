using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeAdvertise : Advertise {

    public string LifingType { get; private set; }
    public string AgeCategory { get; private set; }

    public AgeAdvertise(string[] data)
    {
        this.Name = data[0];
        this.AgeCategory = data[2];
        this.LifingType = data[3];
        this.Price = Int32.Parse(data[4]);
        this.Serialnumber = data[5];
        this.Description = data[6];
    }
}

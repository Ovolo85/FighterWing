using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetAdvertise : Advertise {

    public JetAdvertise(string[] data)
    {
        this.Name = data[0];
        this.Price = Int32.Parse(data[2]);
        this.Serialnumber = data[3];
        this.Description = data[4];
    }

    
}

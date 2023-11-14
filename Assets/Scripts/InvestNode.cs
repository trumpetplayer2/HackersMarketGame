using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestNode : MonoBehaviour
{
    //X Should never change, however X WILL, so we should plan for such
    public float maxY = 0;
    public float minY = 0;
    public void UpdateY(float value, float max, float min)
    {
        if (float.IsNaN(value)) value = 0;
        if (float.IsNaN(max)) max = 1;
        if (max == 0) max = 1;
        if (float.IsNaN(min)) min = 0;
        //Find where the value falls on a scale from Max to Min
        float temp = (value - min) / (max - min) * (maxY - minY) + minY;
        if (float.IsNaN(temp)) temp = 0;
        gameObject.transform.position = new Vector3(transform.position.x, temp, transform.position.z);
    }
}
